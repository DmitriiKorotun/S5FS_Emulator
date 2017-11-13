using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Superblock
    {
        int fsType;
        public int FSType
        {
            get { return fsType; }
            set { fsType = value; }
        }

        int usedBlock;
        public int UsedBlock
        {
            get { return 1; } // x5 short + x1 int
        }

        short clusterSize;
        public short ClusterSize
        {
            get { return clusterSize; }
            set { clusterSize = value; }
        }

        short iNodeCount;
        public short INodeCount
        {
            get { return iNodeCount; }
            set { iNodeCount = value; }
        }

        short iNodeSize;
        public short INodeSize
        {
            get { return iNodeSize; }
            set { iNodeSize = value; }
        }

        short freeBlocks;
        public short FreeBlock
        {
            get { return freeBlocks; }
            set { freeBlocks = value; }
        }

        short freeINode;
        public short FreeINode
        {
            get { return freeINode; }
            set { freeINode = value; }
        }
    }
}
