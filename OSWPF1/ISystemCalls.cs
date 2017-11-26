using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    interface ISystemCalls
    {
        void MakeDir();
        void DelDir();
        void CreateFile();
        void DeleteFile();
        void AddUser();
        void DelUser();
        void AddGroup();
        void DelGroup();
    }
}
