using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirHandler
    {
        public static byte[] WriteFileDirInfo(byte[] block, byte[] data, int startByteNum)
        {
            data.CopyTo(block, startByteNum);
            return block;
        }

        public static void DeleteDir()
        {

        }

        //public static void CreateDir(INode iNode, )
        //{
        //    FSPartsWriter.WriteINode(, iNode);
        //}
    }
}
