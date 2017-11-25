using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class SuperblockWriter
    {
        public static void WriteSuperblock(System.IO.FileStream fs, Superblock superblock)
        {
            fs.Write(BitConverter.GetBytes(superblock.ClusterSize), 0, BitConverter.GetBytes(superblock.ClusterSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FSType), 0, BitConverter.GetBytes(superblock.FSType).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeCount), 0, BitConverter.GetBytes(superblock.INodeCount).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeSize), 0, BitConverter.GetBytes(superblock.INodeSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeBlock), 0, BitConverter.GetBytes(superblock.FreeBlock).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeINode), 0, BitConverter.GetBytes(superblock.FreeINode).Length);
        }
    }
}
