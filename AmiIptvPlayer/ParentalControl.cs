using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiIptvPlayer
{
    public class ParentalControl
    {
        private static ParentalControl instance;

        private List<ChannelInfo> BlockList;
        
        public static ParentalControl Get()
        {
            if (instance == null)
            {
                instance = new ParentalControl();
                instance.BlockList = new List<ChannelInfo>();
            }
            return instance;
        }

        public void Clear()
        {
            BlockList.Clear();
        }

        public bool IsChBlock(ChannelInfo ch)
        {
            return BlockList.Contains(ch);
        }

        public void AddBlockList(ChannelInfo ch)
        {
            if (!BlockList.Contains(ch))
                BlockList.Add(ch);
        }
        public void RemoveBlockList(ChannelInfo ch)
        {
            if (BlockList.Contains(ch))
                BlockList.Remove(ch);
        }

        public object GetBlockList()
        {
            return BlockList;
        }
    }
}
