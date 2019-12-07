using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace start_application
{
    public class RunApp
    {
        public string getFolderPath = ConfigurationManager.AppSettings["AppDataFolder"].ToString();
        public string getAppsFile = ConfigurationManager.AppSettings["AppsFile"].ToString();
        public string cmdString { get; set; }
        XElement element;
        DataSet ds;
        DataTable dt;
        WindowsCommand cmd = new WindowsCommand();

        //Apps File Properties
        private string ID { get; set; }
        private string AppName { get; set; }
        private string AppLoc { get; set; }

        //Default xml
        public string tempXml = @"<Apps>  
                                <App id='1' appname='Google' apploc='http://google.com' /> 
                                <App id='2' appname='MSN' apploc='http://msn.com' />  
                                <App id='3' appname='Yahoo' apploc='http://yahoo.com' /> 
                                </Apps>";

        public void Run()
        {
            //Check Directory
            if (!Directory.Exists(getFolderPath) || getFolderPath.Length < 1)
            {
                Console.WriteLine("\n App folder does not exist! We will create one for you to use.");

                //Create Directory
                string ProjectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string ProjectVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                getFolderPath = @"C:\Temp\" + ProjectName;
                Directory.CreateDirectory(getFolderPath);
                Console.WriteLine("\n App Folder <" + getFolderPath + "> has been created!");
                Console.WriteLine(" Mount this folder at desire location. Then open App.config and enter the path in AppDataFolder value.");

                //Create Temp Apps File
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tempXml);
                getAppsFile = getFolderPath + "\\AppsFile.xml";
                doc.Save(getAppsFile);
                Console.WriteLine("\n Apps File <" + getAppsFile + "> has been created!");
                Console.WriteLine(" Mount the file at desire location. Then open App.config and enter the path in AppsFile value.");

            }

            try
            {
                //Xml validator
                element = XElement.Load(getAppsFile);
                Console.WriteLine("\n File loaded \n");

                //Read Xml
                ds = new DataSet();
                dt = new DataTable();
                ds.ReadXml(getAppsFile);               
                dt = ds.Tables[0];

                //List the file content
                Console.WriteLine(" ---Apps File List--- ");
                dt = ds.Tables[0];
                foreach (DataRow drow in dt.Rows)
                {
                    Console.WriteLine(" " + drow["id"].ToString() + " | " +
                        drow["appname"].ToString() + " | " +
                        drow["apploc"].ToString());
                }

                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n");

                //Now Perform app start operation           
                foreach (DataRow dr in dt.Rows)
                {
                    ID = dr["id"].ToString();
                    AppName = dr["appname"].ToString();
                    AppLoc = dr["apploc"].ToString();

                    if (cmd.StartProcess(AppLoc, getFolderPath))
                        Console.WriteLine(" " + ID + " | " + AppName + " | Started!");
                    else
                        Console.WriteLine(" " + ID + " | " + AppName + " | Failed!");
                }

                //string[] arr = { "http:\\google.com", "http:\\msn.com", @"C:\Users\charl\Documents\Backyard\To Association\Storage Shed.pdf" };
                //getFolderPath = @"C:\Temp\StartApp";
                //cmdString = @"http:\\google.com";
                ////cmd.ExecuteSync(cmdString, getFolderPath);

                //foreach (string s in arr)
                //{
                //    if (cmd.StartProcess(s, getFolderPath))
                //        Console.WriteLine(" " + s + " | Started!");
                //    else
                //        Console.WriteLine(" " + s + " | Failed!");
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                AppLog log = new AppLog();
                log.WriteLog("Error! \n" + ex.Message, getFolderPath + "\\Error");
                //throw ex;
            }


        }

    }
}
