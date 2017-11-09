using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Bitmap
    {
        int usedBlock;
        public int UsedBlock
        {
            get { return 1; } 
        }

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
