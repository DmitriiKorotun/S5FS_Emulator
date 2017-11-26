using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirSeeker
    {
        public static void GetDirs(int nodeNum)
        {

        }

        public static long FindPlaceInDir(int nodeNum)
        {
            long offset = 0;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] != 0)
                    {
                        var block = ByteReader.ReadBlock(fs, 4096, 
                            OffsetHandbook.GetMainDirStart() + node.Di_addr[i] * 4096); //To change 4096
                        bool isFree = true;
                        for (int k = 0; k < 4096; k += 20)
                        {
                            for (int j = 0; j < 20; ++j)
                            {
                                if (block[k + j] == 0)
                                    continue;
                                isFree = false;
                            }
                            if (isFree)
                            {
                                offset = OffsetHandbook.GetMainDirStart() + (node.Di_addr[i] - 1) * 4096 + k;
                            }
                        }
                    }
                }
            }
            return offset;
        }
    }
}
