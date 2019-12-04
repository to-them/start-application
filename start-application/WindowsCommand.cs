using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace start_application
{
    public class WindowsCommand
    {
        AppLog log = new AppLog();

        public void ExecuteSync(object command, string LogFolderPath)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;

                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;

                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                // Display the command output or write to file.
                log.WriteLog(result, LogFolderPath + "\\Result");
            }
            catch (Exception ex)
            {
                // Log the exception
                log.WriteLog(ex.Message, LogFolderPath + "\\Error");
                throw ex;
            }
        }

        public bool StartProcess(string app, string log_folder_path)
        {
            try
            {
                Process.Start(app);
                log.WriteLog(app + " | Started!", log_folder_path + "\\Result");
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                log.WriteLog(app + " | Error! \n" + ex.Message, log_folder_path + "\\Error");
                return false;
                //throw ex;
            }

        }
    }
}
