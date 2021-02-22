using AmiIptvPlayer.i18n;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AmiIptvPlayer
{

    public class SeenResumeChannels
    {
        private static SeenResumeChannels instance;
        public static SeenResumeChannels Get()
        {
            if (instance == null)
                instance = new SeenResumeChannels();
            return instance;
        }
        public SeenResumeChannels()
        {
            channelsSeenResume = new Dictionary<string, List<SeenResumeChannel>>();
            channelsSeenResume["resume"] = new List<SeenResumeChannel>();
            channelsSeenResume["seen"] = new List<SeenResumeChannel>();
        }
        public Dictionary<string, List<SeenResumeChannel>> channelsSeenResume { get; set; }

        public void Set(SeenResumeChannels items)
        {
            instance = items;
        }

        public bool IsSeen(string title)
        {
            foreach(var seenItem in channelsSeenResume["seen"])
            {
                if (seenItem.title == title && seenItem.seen)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsResume(string title)
        {
            foreach (var resumeItem in channelsSeenResume["resume"])
            {
                if (resumeItem.title == title && resumeItem.position>150)
                {
                    return true;
                }
            }
            return false;
        }

        public double? GetCurrentPosition(string title)
        {
            foreach (var resumeItem in channelsSeenResume["resume"])
            {
                if (resumeItem.title == title)
                {
                    return resumeItem.position;
                }
            }
            return null;
        }

        
        public void UpdateOrSetSeen(string title, bool value, double totalSeconds, DateTime now)
        {
            List<SeenResumeChannel> srch = GetSeenChannelsName(title);
            if (srch.Count == 0)
            {
                channelsSeenResume["seen"].Add(new SeenResumeChannel() { seen = value, position = 0, title = title, date = now, totalDuration=totalSeconds});
            }
            else
            {
                foreach(var item in srch)
                {
                    item.seen = value;
                    item.date = now;
                    if (item.totalDuration > 0)
                        item.totalDuration = totalSeconds;
                }
            }
        }

        public void UpdateOrSetResume(string title, double value, double totalSeconds, DateTime now)
        {
            List<SeenResumeChannel> srch = GetResumeChannelsName(title);
            if (srch.Count == 0)
            {
                channelsSeenResume["resume"].Add(new SeenResumeChannel() { seen = false, position = value, title = title, date= now, totalDuration=totalSeconds });
            }
            else
            {
                foreach (var item in srch)
                {
                    item.position = value;
                    item.date = now;
                    if (item.totalDuration>0)
                        item.totalDuration = totalSeconds;
                }
            }
        }

        public double? GetCurrentTotalDuration(string title)
        {
            foreach (var resumeItem in channelsSeenResume["resume"])
            {
                if (resumeItem.title == title)
                {
                    return resumeItem.totalDuration;
                }
            }
            return null;
        }

        private List<SeenResumeChannel> GetSeenChannelsName(string title)
        {
            List<SeenResumeChannel> returnvalue = new List<SeenResumeChannel>();
            foreach (var srch in channelsSeenResume["seen"])
            {
                if (srch.title == title)
                    returnvalue.Add(srch);
            }
            return returnvalue;
        }
        private List<SeenResumeChannel> GetResumeChannelsName(string title)
        {
            List<SeenResumeChannel> returnvalue = new List<SeenResumeChannel>();
            foreach (var srch in channelsSeenResume["resume"])
            {
                if (srch.title == title)
                    returnvalue.Add(srch);
            }
            return returnvalue;
        }

        public void RemoveResume(string title)
        {
            List<SeenResumeChannel> newlist = new List<SeenResumeChannel>();
            foreach (var srch in channelsSeenResume["resume"])
            {
                if (srch.title != title)
                    newlist.Add(srch);
            }
            channelsSeenResume["resume"] = newlist;
        }

        public void RemoveSeen(string title)
        {
            List<SeenResumeChannel> newlist = new List<SeenResumeChannel>();
            foreach (var srch in channelsSeenResume["seen"])
            {
                if (srch.title != title)
                    newlist.Add(srch);
            }
            channelsSeenResume["seen"] = newlist;
        }
    }

    public class SeenResumeChannel
    {
        public string title { get; set; }
        public double position { get; set; }
        public double totalDuration{ get; set; }
        public bool seen { get; set; }
        public DateTime date{ get; set; }

    }
    public class AmiConfiguration
    {
        public AmiConfiguration()
        {
            availableLangs.Add("SYSTEM", 0);
            availableLangs.Add("es-ES", 1);
            availableLangs.Add("en-US", 2);
        }
        private static AmiConfiguration instance;
        public Dictionary<string, int> availableLangs = new Dictionary<string, int>();
        public static AmiConfiguration Get()
        {
            if (instance == null)
                instance = new AmiConfiguration();
            return instance;
        }
        public static void SetInstance(AmiConfiguration value)
        {
            instance = value;
        }
        public string URL_IPTV { get; set; }
        public string URL_EPG { get; set; }
        public string UI_LANG { get; set; }
        public string DEF_LANG { get; set; }
        public string DEF_SUB { get; set; }

    }

    public class IPTVData
    {
       
        private static IPTVData instance;
        public static IPTVData Get()
        {
            if (instance == null)
                instance = new IPTVData();
            return instance;
        }
        public string USER { get; set; }
        public string MAX_CONECTIONS { get; set; }
        public string ACTIVE_CONECTIONS{ get; set; }
        public DateTime EXPIRE_DATE{ get; set; }
        public string HOST { get; set; }
        public int PORT { get; set; }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public long Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class Utils
    {
        public static string LastSearch = "";
        public static string PosterBasePath = "https://image.tmdb.org/t/p/original";
        public static Dictionary<string, string> audios = new Dictionary<string, string>
        {
            { "spa", Strings.AS_SP},
            { "eng", Strings.AS_EN },
            { "unk", Strings.AS_UN }
        };
        public static Dictionary<string, string> subs = new Dictionary<string, string>
        {
            { "none", Strings.AS_NONE},
            { "spa", Strings.AS_SP},
            { "eng", Strings.AS_EN }
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

        public static string GetUrl(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
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

        public static List<SearchIdent> TransformJArrayToSearchIdent (JArray fillFilmResults, ChType type) {
            List<SearchIdent> listSearch = new List<SearchIdent>();
            foreach(JObject obj in fillFilmResults)
            {
                string title = "";
                string year = "1900";
                if (type == ChType.MOVIE)
                {
                    title = obj["title"].ToString();
                    year = obj["release_date"]?.ToString().Split('-')[0];
                }
                else
                {
                    title = obj["name"].ToString();
                    year = obj["first_air_date"]?.ToString().Split('-')[0];                   
                }
                SearchIdent se = new SearchIdent();
                se.Title = title;
                se.Year = year;
                se.SearchData = obj;
                se.ChType = type;
                listSearch.Add(se);
            }
            return listSearch;
        }

        public static dynamic GetFilmInfo(ChannelInfo channel, string lang)
        {
            string name = channel.Title;
            string movie_date = "";
            if (Regex.IsMatch(name, @"\(\d\d\d\d\)\s*?$")){
                movie_date = name.Substring(name.Length - 6, name.Length - (name.Length - 6)).Trim();
                name = name.Substring(0, name.Length - 7);                
            }
            if (Regex.IsMatch(name, @"\-\s*?\d\d\d\d\s*?$"))
            {
                movie_date = name.Substring(name.Length - 5, name.Length - (name.Length - 5)).Trim();
                name = name.Substring(0, name.Length - 7);                
            }
            if (Regex.IsMatch(name, @"\s*?S\d\d\sE\d\d\s*?$"))
            {
                movie_date = null;
                name = name.Substring(0, name.Length - 7);
            }
            LastSearch = name;
            return GetFilmInfo(channel.ChannelType, name, movie_date, lang);
        }

        public static void GetAccountInfo()
        {
            string url = AmiConfiguration.Get().URL_IPTV;
            IPTVData data = IPTVData.Get();
            Uri uri = new Uri(AmiConfiguration.Get().URL_IPTV);
            data.HOST = uri.Host;
            data.PORT = uri.Port;
            data.USER = HttpUtility.ParseQueryString(uri.Query).Get("username");
            if (string.IsNullOrEmpty(data.USER))
            {
                data.USER = Strings.UNKNOWN;
            }
            data.MAX_CONECTIONS = "0";
            try
            {
                if (url.Contains("get.php"))
                {
                    url = url.Replace("get.php", "player_api.php");
                    string result = GetUrl(url);
                    dynamic dataServer = JsonConvert.DeserializeObject(result);
                    data.EXPIRE_DATE = UnixToDate(int.Parse(dataServer["user_info"]["exp_date"].Value.ToString()));
                    data.USER = dataServer["user_info"]["username"].Value.ToString();
                    data.MAX_CONECTIONS = dataServer["user_info"]["max_connections"].Value.ToString();
                    data.ACTIVE_CONECTIONS = dataServer["user_info"]["active_cons"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(Strings.ACCOUNT_INFO_ERROR, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static DateTime UnixToDate(int Timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
            
        }

        public static Dictionary<string, JArray> GetFilmInfo( string title, string year, string lang)
        {
            var result = new Dictionary<string, JArray>() { };
            
            var movies = GetFilmInfo(ChType.CHANNEL, title, year, lang);
            result["movies"] = movies["results"];
            var shows = GetFilmInfo(ChType.SHOW, title, year, lang);
            result["shows"] = shows["results"];
            return result;
        }

        public static dynamic GetFilmInfo(ChType chType, string title, string year, string lang)
        {
            string apiUrl = "https://api.themoviedb.org/3/search/$$FTYPE$$?api_key=$$APIKEY$$&language=$$LANG$$&query=$$NAME$$&$$YEAR$$page=1&include_adult=false";
            string apiKey = "9e92adff436095fb58d51262de09385a";
            string ftype = "movie";
            string yearStr = "year=$$YEAR$$";
            string name = title;
            string movie_date = year;
            if (chType == ChType.SHOW)
            {
                ftype = "tv";
                yearStr = "first_air_date_year=$$YEAR$$";
            }
            apiUrl = apiUrl.Replace("$$FTYPE$$", ftype).Replace("$$APIKEY$$", apiKey).Replace("$$LANG$$", lang);
            if (!string.IsNullOrEmpty(movie_date) && Regex.IsMatch(movie_date, @"\d\d\d\d"))
            {
                apiUrl = apiUrl.Replace("$$YEAR$$", yearStr).Replace("$$YEAR$$", movie_date + "&");
            }
            string nameWithPercent = name.Replace(" ", "%20");
            apiUrl = apiUrl.Replace("$$NAME$$", nameWithPercent);
            string result = GetUrl(apiUrl);
            return JsonConvert.DeserializeObject(result);
        }

        public static string LogoUrl(string logoFile)
        {
            return "https://image.tmdb.org/t/p/w200" + logoFile;
        }

        public static string DecodeToUTF8(string strParam)
        {
            byte[] bytes = Encoding.Default.GetBytes(strParam);
            strParam = Encoding.UTF8.GetString(bytes);
            return strParam;
        }
    }
}
