using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Bitmap
    {
        // How many blocks are given to this data
        int usedBlock;
        public int UsedBlock
        {
            get { return 1; } 
        }

        // Bitmap of FS
        byte[] bitmapValue;
        public byte[] BitmapValue
        {
            get
            {
                if (bitmapValue == null)
                    bitmapValue = new byte[1600];
                return bitmapValue;
            }
            set { bitmapValue = value; }
        }
    }
}
