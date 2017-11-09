using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class INodeMap
    {
        int offset;
        public int Offset
        {
            get { return 3200; } //x1600 short
        }

        short[] nodeAdress;
        public short[] NodeAdress
        {
            get
            {
                if (nodeAdress == null)
                    nodeAdress = new short[1600];
                return nodeAdress;
            }
            set { nodeAdress = value; }
        }
    }
}
