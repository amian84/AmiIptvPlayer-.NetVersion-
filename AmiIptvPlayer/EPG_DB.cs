
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;

namespace AmiIptvPlayer
{

    public class EPGEventArgs : EventArgs
    {
        public bool Error { get; set; }
        
    }

    public class EPG_DB
    {
        private Thread parseThread;
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
            bool deleteJson = false;
            using (StreamReader r = new StreamReader(Utils.CONF_PATH + "amiiptvepgCache.json"))
            {
                try
                {
                    string json = r.ReadToEnd();
                    _instance.DB = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, Dictionary<int, Dictionary<int, List<PrgInfo>>>>>>(json);
                    EPGEventArgs epgEvent = new EPGEventArgs();
                    epgEvent.Error = false;
                    _instance.epgEventFinish(_instance, epgEvent);
                    _instance.Loaded = true;
                }catch (Exception ex)
                {
                    deleteJson = true;
                    _instance.Loaded = false;
                    Console.WriteLine("Error trying read epg json: " + ex.ToString());
                    
                }
                
            }
            if (deleteJson)
            {
                File.Delete(Utils.CONF_PATH + "amiiptvepgCache.json");
            }
            
            return _instance;
        }

        private void Parse(){
            EPGEventArgs epgEvent = new EPGEventArgs();
            epgEvent.Error = false;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(Utils.CONF_PATH + "amiiptvepg.xml");
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
                using (StreamWriter file = File.CreateText(Utils.CONF_PATH + "amiiptvepgCache.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, DB);
                }
                epgEventFinish?.Invoke(this, epgEvent);
                Loaded = true;
            }catch (Exception ex)
            {
                File.Delete(Utils.CONF_PATH + "amiiptvepg.xml");
                epgEvent.Error = true;
                epgEventFinish?.Invoke(this, epgEvent);
                Loaded = false;
            }
            
        }

        public void ParseDB()
        {
            parseThread = new System.Threading.Thread(delegate ()
            {
                Parse();

            });
            parseThread.IsBackground = true;
            parseThread.Start();
            
        }

        public void Stop()
        {
            if (parseThread != null && parseThread.IsAlive)
            {
                parseThread.Abort();    
            }
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
