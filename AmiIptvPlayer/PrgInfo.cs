using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class PrgInfo
    {
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public List<string> Categories { get; set; }
        public string Country { get; set; }
        public string Stars { get; set; }
        public string Rating { get; set; }
        public PrgInfo()
        {
        }

    }
}
