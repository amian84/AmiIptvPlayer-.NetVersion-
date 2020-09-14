using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class EPG_DB
    {
        private static EPG_DB _instance;
        public bool Refresh { get; set; }
        public static EPG_DB Get()
        {
            if (_instance == null)
            {
                _instance = new EPG_DB();
            }
            return _instance;
        }
    }
}
