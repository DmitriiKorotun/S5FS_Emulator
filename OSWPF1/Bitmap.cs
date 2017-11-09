using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Bitmap
    {
        int offset;
        public int Offset
        {
            get { return 1600; } //x1600 byte
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
