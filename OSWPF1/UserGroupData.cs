using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class UserGroup
    {
        Dictionary<string, List<string>> data;
        public Dictionary<string, List<string>> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
