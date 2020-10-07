using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class Utils
    {
        public static Dictionary<string, string> audios = new Dictionary<string, string>
        {
            { "spa", "Spanish"},
            { "eng", "English" }
        };
        public static Dictionary<string, string> subs = new Dictionary<string, string>
        {
            { "none", "None"},
            { "spa", "Spanish"},
            { "eng", "English" }
        };
        public static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static string GetAudioConfName(string audioCombo)
        {
            foreach (KeyValuePair<string, string> entry in audios)
            {
                if (entry.Value == audioCombo)
                {
                    return entry.Key;
                }
            }
            return "spa";
        }

        public static string GetSubConfName(string subCombo)
        {
            foreach (KeyValuePair<string, string> entry in subs)
            {
                if (entry.Value == subCombo)
                {
                    return entry.Key;
                }
            }
            return "none";
        }
    }
}
