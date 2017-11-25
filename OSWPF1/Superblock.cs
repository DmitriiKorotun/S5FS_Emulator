using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Superblock
    {
        public Superblock()
        {

        }

        public Superblock(IniFiles.IniHandler dataFile)
        {
            this.ClusterSize = Convert.ToInt16(dataFile.ReadINI("Superblock", "BlockSize"));
            byte[] ftype = Encoding.ASCII.GetBytes(dataFile.ReadINI("Superblock", "Name"));
            this.FSType = BitConverter.ToInt32(ftype, 0);
            this.INodeCount = Convert.ToInt16(dataFile.ReadINI("Superblock", "INodeCount"));
            this.INodeSize = Convert.ToInt16(dataFile.ReadINI("Superblock", "INodeSize"));
            this.FreeBlock = Convert.ToInt16(dataFile.ReadINI("Superblock", "FreeBlocksCount"));
            this.FreeINode = Convert.ToInt16(dataFile.ReadINI("Superblock", "FreeINodeCount"));
        }

        int fsType;
        public int FSType
        {
            get { return fsType; }
            set { fsType = value; }
        }


        static int offset;
        public static int Offset
        {
            get { return 14; }
        }

        static int usedBlock;
        public static int UsedBlock
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
