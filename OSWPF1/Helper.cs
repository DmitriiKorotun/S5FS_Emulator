using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Helper
    {
        public void WriteINode(System.IO.FileStream fs, INode iNode, long offset)
        {
            fs.Position = offset;
            FSPartsWriter.WriteINode(fs, iNode);
        }

        public void WriteBlock()
        {

        }

    }
}
