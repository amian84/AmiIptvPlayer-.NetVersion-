using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AmiIptvPlayer
{
    public class EPG_DB
    {
        private static EPG_DB _instance;
        public bool Refresh { get; set; }
        private Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>> DB = new Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>>();
        private Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>> skelDB = new Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>();
        private EPG_DB()
        {
            DateTime now = DateTime.Now;
            
        }
        public static EPG_DB Get()
        {
            if (_instance == null)
            {
                _instance = new EPG_DB();
            }
            return _instance;
        }

        public void ParseDB()
        {
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
                    DB[channelID] = skelDB;
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
            var hola = 2;
        }
    }
}
