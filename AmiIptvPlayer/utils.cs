using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class Utils
    {
        public static string PosterBasePath = "https://image.tmdb.org/t/p/original";
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

        public static string GetUrl(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return  reader.ReadToEnd();
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

        public static dynamic GetFilmInfo(ChannelInfo channel, string lang)
        {
            
            string apiUrl = "https://api.themoviedb.org/3/search/$$FTYPE$$?api_key=$$APIKEY$$&language=$$LANG$$&query=$$NAME$$&page=1&include_adult=false";
            string apiKey = "9e92adff436095fb58d51262de09385a";
            string ftype = "movie";
            string name = channel.Title;
            if (Regex.IsMatch(name, @"\(\d\d\d\d\)\s*?$")){
                name = name.Substring(0, name.Length - 7);
            }
            if (channel.ChannelType == ChType.SHOW)
            {
                ftype = "tv";
                name = channel.TVGGroup;
            }
            apiUrl = apiUrl.Replace("$$FTYPE$$", ftype).Replace("$$APIKEY$$", apiKey).Replace("$$LANG$$", lang);

            string nameWithPercent = name.Replace(" ", "%20");
            apiUrl = apiUrl.Replace("$$NAME$$", nameWithPercent);
            string result =  GetUrl(apiUrl);
            return JsonConvert.DeserializeObject(result);
        }
    }
}
