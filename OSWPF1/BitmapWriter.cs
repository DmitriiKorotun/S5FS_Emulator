using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class BitmapWriter
    {
        public static long WriteBitmap(System.IO.FileStream fs, Bitmap bitmap)
        {
            long bytesWritten = 0;
            foreach (byte byteElem in bitmap.BitmapValue)
            {
                fs.WriteByte(byteElem);
                ++bytesWritten;
            }
            return bytesWritten;
        }
    }
}
