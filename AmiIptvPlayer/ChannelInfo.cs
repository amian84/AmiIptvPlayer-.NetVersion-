using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class ChannelInfo
    {
        public string Title { get; set; }
        public string TVGName { get; set; }
        public string TVGId { get; set; }
        public string TVGGroup { get; set; }
        public string TVGLogo { get; set; }
        public string URL { get; set; }
        public int ChNumber { get; set; }

        public ChannelInfo()
        {
        }

        public ChannelInfo(M3uPlaylistEntry channelEntry)
        {
            URL = channelEntry.Path;
            string extraInfoForParser = "";
            foreach (KeyValuePair<string, string> entry in channelEntry.CustomProperties)
            {
                extraInfoForParser += entry.Key + ":" + entry.Value;
            }
            string regexTVGName = "tvg-name=\"(.*?)\"";
            string regexTVGLogo = "tvg-logo=\"(.*?)\"";
            string regexTVGGroup = "group-title=\"(.*?)\"";
            string regexTVGId = "tvg-id=\"(.*?)\"";
            string regexTitle = "[,](?!.*[,])(.*?)$";
            TVGName = MatchAndResult(extraInfoForParser, regexTVGName);
            Title = MatchAndResult(extraInfoForParser, regexTitle);
            TVGLogo = MatchAndResult(extraInfoForParser, regexTVGLogo);
            TVGGroup = MatchAndResult(extraInfoForParser, regexTVGGroup);
            TVGId = MatchAndResult(extraInfoForParser, regexTVGId);

        }
        private string MatchAndResult (string toSearch, string pattern)
        {
            Match result = Regex.Match(toSearch, pattern);
            if (result.Success)
            {
                return result.Groups[1].Value;
            }
            return "";
        }
    }
}
