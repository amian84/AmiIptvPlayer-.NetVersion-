
using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class Channels
    {
        private static Channels instance;
        private bool repaintList = false;
        private Dictionary<int, ChannelInfo> channelsInfo = new Dictionary<int, ChannelInfo>();
        public static Channels Get()
        {
            if (instance == null)
            {
                instance = new Channels();
            }
            return instance;
        }
        public void RefreshList(string _url)
        {
            string contents;
            using (var wc = new WebClient())
            {
                contents = wc.DownloadString(_url);
            }
            
            var parser = PlaylistParserFactory.GetPlaylistParser(".m3u");
            IBasePlaylist playlist = parser.GetFromString(contents);
            M3uPlaylist m3uList = (M3uPlaylist)playlist;
            int channelNumber = 0;
            foreach(M3uPlaylistEntry entry in m3uList.PlaylistEntries)
            {
                ChannelInfo channelInfo = new ChannelInfo(entry);
                channelsInfo.Add(channelNumber, channelInfo);
                channelNumber++;

            }
            this.repaintList = true;
        }
        public void SetRepaint(bool value)
        {
            this.repaintList = value;
        }

        public bool RePaint()
        {
            return this.repaintList;
        }
    }
}
