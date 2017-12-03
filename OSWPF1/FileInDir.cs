using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileInDir
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        short nodeNum;
        public short NodeNum
        {
            get { return nodeNum; }
            set { nodeNum = value; }
        }

        bool type;
        public bool Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
