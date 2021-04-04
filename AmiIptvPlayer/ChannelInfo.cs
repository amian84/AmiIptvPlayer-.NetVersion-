using PlaylistsNET.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AmiIptvPlayer
{

    public enum ChType
    {
        UNKNOWN,
        CHANNEL,
        MOVIE,
        SHOW
    }

    public class ChannelInfo
    {
        public string Title { get; set; }
        public string TVGName { get; set; }
        public string TVGId { get; set; }
        public string TVGGroup { get; set; }
        public string TVGLogo { get; set; }
        public string URL { get; set; }
        public int ChNumber { get; set; }
        public ChType ChannelType { get; set; }
        public bool seen { get; set; }
        public double? currentPostion { get; set; }
        public double? totalDuration { get; set; }
        public ChannelInfo()
        {
            seen = false;
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
            Title = Utils.DecodeToUTF8(MatchAndResult(extraInfoForParser, regexTitle));
            TVGLogo = MatchAndResult(extraInfoForParser, regexTVGLogo);
            TVGGroup = Utils.DecodeToUTF8(MatchAndResult(extraInfoForParser, regexTVGGroup));
            TVGId = MatchAndResult(extraInfoForParser, regexTVGId);
            CalculateType();


        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ChannelInfo))
                return false;
            ChannelInfo compare = (ChannelInfo)obj;
            return compare.Title == Title
                && compare.TVGGroup == TVGGroup
                && compare.TVGName == TVGName
                && compare.TVGId == TVGId;
        }
        public void CalculateType()
        {
            if (URL.EndsWith(".mkv") || URL.EndsWith(".avi") || URL.EndsWith(".mp4") || URL.EndsWith(".m3u8"))
            {
                ChannelType = ChType.MOVIE;
                if (Regex.IsMatch(Title, @"S\d\d\s*?E\d\d$"))
                {
                    ChannelType = ChType.SHOW;
                }
            }
            else
            {
                ChannelType = ChType.CHANNEL;
            }
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
