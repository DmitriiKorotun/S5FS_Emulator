using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DateWorker
    {
        public static string GetDate(DateTime date)
        {
            string dateStr = date.ToString().Substring(0, 10);
            dateStr = dateStr.Remove(5, 1);
            dateStr = dateStr.Remove(2, 1);
            return dateStr;
        }
    }
}
