using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class File
    {
        long size;
        public long Size
        {
            get { return size; }
            set { size = value; }
        }

        int freeBlocks;
        public int FreeBlocks
        {
            get { return freeBlocks; }
            set { freeBlocks = value; }
        }
    }
}
