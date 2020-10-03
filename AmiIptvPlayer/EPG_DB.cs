
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AmiIptvPlayer
{

    public class EPGEventArgs : EventArgs
    {
        public bool Error { get; set; }
        
    }

    public class EPG_DB
    {
        public event FinishParsed epgEventFinish;
        public delegate void FinishParsed(EPG_DB epg, EPGEventArgs e);
        private static EPG_DB _instance;
        public bool Refresh { get; set; }
        public bool Loaded{ get; set; }
        private Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>> DB = new Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>>();
        
        private EPG_DB()
        {
            DateTime now = DateTime.Now;

        }
        public static EPG_DB Get()
        {
            if (_instance == null)
            {
                _instance = new EPG_DB();
                _instance.Loaded = false;
            }
            return _instance;
        }
        public static EPG_DB LoadFromJSON()
        {
            if (_instance == null)
            {
                _instance = new EPG_DB();
            }
            using (StreamReader r = new StreamReader(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json"))
            {
                string json = r.ReadToEnd();
                _instance.DB = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>>>(json);
                EPGEventArgs epgEvent = new EPGEventArgs();
                epgEvent.Error = false;
                _instance.epgEventFinish(_instance, epgEvent);
                _instance.Loaded = true;
            }
            return _instance;
        }

        private void Parse(){
            EPGEventArgs epgEvent = new EPGEventArgs();
            epgEvent.Error = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepg.xml");
            XmlNodeList list_programs = doc.SelectNodes("/tv/programme");
            foreach (XmlNode prg in list_programs)
            {
                PrgInfo prginfo = new PrgInfo();
                string formatString = "yyyyMMddHHmmss";
                prginfo.StartTime = DateTime.ParseExact(prg.Attributes["start"].Value.Split(' ')[0], formatString, CultureInfo.InvariantCulture);
                prginfo.StopTime = DateTime.ParseExact(prg.Attributes["stop"].Value.Split(' ')[0], formatString, CultureInfo.InvariantCulture);
                string channelID = prg.Attributes["channel"].Value;

                prginfo.Title = prg.SelectSingleNode("title").InnerText;
                try
                {
                    prginfo.Description = prg.SelectSingleNode("desc").InnerText;
                }
                catch (Exception ex)
                {
                    prginfo.Description = "No Description";
                }

                XmlNodeList categories = prg.SelectNodes("category");
                foreach (XmlNode cat in categories)
                {
                    if (prginfo.Categories == null)
                    {
                        prginfo.Categories = new List<string>();
                    }
                    prginfo.Categories.Add(cat.InnerText);
                }
                try
                {
                    prginfo.Stars = prg.SelectSingleNode("star-rating").InnerText;
                }
                catch (Exception ex)
                {
                    prginfo.Stars = "-";
                }

                try
                {
                    prginfo.Logo = prg.SelectSingleNode("icon").Attributes["src"].InnerText;
                }
                catch (Exception ex)
                {
                    prginfo.Logo = null;
                }
                try
                {
                    prginfo.Country = prg.SelectSingleNode("country").InnerText;
                }
                catch (Exception ex)
                {
                    prginfo.Country = "-";
                }
                try
                {
                    prginfo.Rating = prg.SelectSingleNode("review").InnerText;
                }
                catch (Exception ex)
                {
                    prginfo.Rating = "-";
                }

                if (!DB.ContainsKey(channelID))
                {
                    DB[channelID] = new Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>();
                }
                if (!DB[channelID].ContainsKey(prginfo.StartTime.Year))
                {
                    DB[channelID][prginfo.StartTime.Year] = new Dictionary<int, Dictionary<int, List<PrgInfo>>>();
                }
                if (!DB[channelID][prginfo.StartTime.Year].ContainsKey(prginfo.StartTime.Month))
                {
                    DB[channelID][prginfo.StartTime.Year][prginfo.StartTime.Month] = new Dictionary<int, List<PrgInfo>>();
                }
                if (!DB[channelID][prginfo.StartTime.Year][prginfo.StartTime.Month].ContainsKey(prginfo.StartTime.Day))
                {
                    DB[channelID][prginfo.StartTime.Year][prginfo.StartTime.Month][prginfo.StartTime.Day] = new List<PrgInfo>();
                }
                DB[channelID][prginfo.StartTime.Year][prginfo.StartTime.Month][prginfo.StartTime.Day].Add(prginfo);
            }
            using (StreamWriter file = File.CreateText(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\amiiptvepgCache.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, DB);
            }
            epgEventFinish(this, epgEvent);
            Loaded = true;
        }

        public void ParseDB()
        {
            new System.Threading.Thread(delegate ()
            {
                Parse();

            }).Start();
        }

        public PrgInfo GetCurrentProgramm(ChannelInfo channel)
        {

            PrgInfo result = null;
            DateTime nowDate = DateTime.Now;
            List<PrgInfo> listProgramms = null;
            try
            {
                listProgramms = DB[channel.TVGId][nowDate.Year][nowDate.Month][nowDate.Day];
            } catch (Exception ex)
            {
                listProgramms = new List<PrgInfo>();
            }

            foreach (PrgInfo prg in listProgramms)
            {
                if (prg.StartTime <= nowDate)
                {
                    if (prg.StopTime >= nowDate)
                    {
                        result = prg;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
