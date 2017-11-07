using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Bitmap
    {
        short[] bitmapValue;
        public short[] BitmapValue
        {
            get
            {
                if (bitmapValue == null)
                    bitmapValue = new short[1600];
                return bitmapValue;
            }
            set { bitmapValue = value; }
        }
    }
}
