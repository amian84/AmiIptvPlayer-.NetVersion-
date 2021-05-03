using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmiIptvPlayer.Tools
{
	public abstract class Logger : IDisposable
	{
		private Queue<Action> _queue = new Queue<Action>();
		private ManualResetEvent _hasNewItems = new ManualResetEvent(false);
		private ManualResetEvent _terminate = new ManualResetEvent(false);
		private ManualResetEvent _waiting = new ManualResetEvent(false);
		private Thread _loggingThread;
		protected string BasePath;

		private static readonly Lazy<Logger> _lazyLog = new Lazy<Logger>(() => {
			return new LocalLogger();			
		});

		public static Logger Current => _lazyLog.Value;

		protected Logger()
		{
			_loggingThread = new Thread(new ThreadStart(ProcessQueue)) { IsBackground = true };
			_loggingThread.Start();
		}
		public void SetBasePath(string value)
        {
			BasePath = value;
        }

		public void Info(string message)
		{
			Log(message, LogType.INF);
		}

		public void Debug(string message)
		{
			Log(message, LogType.DBG);
		}

		public void Error(string message)
		{
			Log(message, LogType.ERR);
		}

		public void Error(Exception e)
		{
			Log(UnwrapExceptionMessages(e), LogType.ERR);
			
		}

		public override string ToString() => $"Logger settings: [Type: {this.GetType().Name}";

		protected abstract void CreateLog(string message);

		public void Flush() => _waiting.WaitOne();

		public void Dispose()
		{
			_terminate.Set();
			_loggingThread.Join();
		}

		protected virtual string ComposeLogRow(string message, LogType logType) =>
			$"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)} {logType}] - {message}";

		protected virtual string UnwrapExceptionMessages(Exception ex)
		{
			if (ex == null)
				return string.Empty;

			return $"{ex}, Inner exception: {UnwrapExceptionMessages(ex.InnerException)} ";
		}

		private void ProcessQueue()
		{
			while (true)
			{
				_waiting.Set();
				int i = WaitHandle.WaitAny(new WaitHandle[] { _hasNewItems, _terminate });
				if (i == 1) return;
				_hasNewItems.Reset();
				_waiting.Reset();

				Queue<Action> queueCopy;
				lock (_queue)
				{
					queueCopy = new Queue<Action>(_queue);
					_queue.Clear();
				}

				foreach (var log in queueCopy)
				{
					log();
				}
			}
		}

		private void Log(string message, LogType logType)
		{
			if (string.IsNullOrEmpty(message))
				return;

			var logRow = ComposeLogRow(message, logType);
			System.Diagnostics.Debug.WriteLine(logRow);

			lock (_queue)
				_queue.Enqueue(() => CreateLog(logRow));

			_hasNewItems.Set();
			
		}
	}

	class LocalLogger : Logger
	{
		private const string LogFolderName = "Logs";
		private const string LogFileName = "amiiptvplayer.log";
		private readonly int _logChunkSize = 1000000;
		private readonly int _logChunkMaxCount = 5;
		private readonly int _logArchiveMaxCount = 20;
		private readonly int _logCleanupPeriod = 7;

		protected override void CreateLog(string message)
		{
			var logFolderPath = Path.Combine((string.IsNullOrEmpty(BasePath)) ? Path.GetTempPath(): BasePath, LogFolderName);
			if (!Directory.Exists(logFolderPath))
				Directory.CreateDirectory(logFolderPath);

			var logFilePath = Path.Combine(logFolderPath, LogFileName);

			Rotate(logFilePath);

			using (var sw = File.AppendText(logFilePath))
			{
				sw.WriteLine(message);
			}
		}

		private void Rotate(string filePath)
		{
			if (!File.Exists(filePath))
				return;

			var fileInfo = new FileInfo(filePath);
			if (fileInfo.Length < _logChunkSize)
				return;

			var fileTime = DateTime.Now.ToString("dd_MM_yy_h_m_s");
			var rotatedPath = filePath.Replace(".log", $".{fileTime}");
			File.Move(filePath, rotatedPath);

			var folderPath = Path.GetDirectoryName(rotatedPath);
			var logFolderContent = new DirectoryInfo(folderPath).GetFileSystemInfos();

			var chunks = logFolderContent.Where(x => !x.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase));

			if (chunks.Count() <= _logChunkMaxCount)
				return;

			var archiveFolderInfo = Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(rotatedPath), $"{LogFolderName}_{fileTime}"));

			foreach (var chunk in chunks)
			{
				Directory.Move(chunk.FullName, Path.Combine(archiveFolderInfo.FullName, chunk.Name));
			}

			ZipFile.CreateFromDirectory(archiveFolderInfo.FullName, Path.Combine(folderPath, $"{LogFolderName}_{fileTime}.zip"));
			Directory.Delete(archiveFolderInfo.FullName, true);

			var archives = logFolderContent.Where(x => x.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase)).ToArray();

			if (archives.Count() <= _logArchiveMaxCount)
				return;

			var oldestArchive = archives.OrderBy(x => x.CreationTime).First();
			var cleanupDate = oldestArchive.CreationTime.AddDays(_logCleanupPeriod);
			if (DateTime.Compare(cleanupDate, DateTime.Now) <= 0)
			{
				foreach (var file in logFolderContent)
				{
					file.Delete();
				}
			}
			else
				File.Delete(oldestArchive.FullName);

		}

		public override string ToString() => $"{base.ToString()}, Chunk Size: {_logChunkSize}, Max chunk count: {_logChunkMaxCount}, Max log archive count: {_logArchiveMaxCount}, Cleanup period: {_logCleanupPeriod} days]";
	}

	


	public enum LogType
	{
		INF,
		DBG,
		ERR
	}

}
