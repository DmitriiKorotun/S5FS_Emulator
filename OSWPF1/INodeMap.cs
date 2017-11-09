using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class INodeMap
    {
        int usedBlock;
        public int UsedBlock
        {
            get { return 1; } //x1600 short
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
