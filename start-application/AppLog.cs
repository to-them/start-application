using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace start_application
{
    public class AppLog
    {
        public void WriteLog(string message, string LogFilePath)
        {
            string _sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString();
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string _LogTime = "_" + sMonth + "-" + sDay + "-" + sYear + ".txt";

            StreamWriter sw = new StreamWriter(LogFilePath + _LogTime, true);
            sw.WriteLine(_sLogFormat + " | " + message);
            sw.Flush();
            sw.Close();
        }

    }
}
