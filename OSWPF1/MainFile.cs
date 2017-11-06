using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class MainFile
    {
        public string CreateFile(string name)
        {
            if (System.IO.File.Exists(name))
            {
                return "File is already exists";
            }
            else
            {
                using (System.IO.FileStream fs = System.IO.File.Create(name))
                {
                    for (long i = 0; i < 1024; ++i) //62914560
                    {
                        fs.WriteByte(48);
                    }
                }
                return "Succeed. File was created. Path: " + name;
            }
        }
    }
}
