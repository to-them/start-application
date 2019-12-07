using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace start_application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("\t Applications Startup...");
            Console.WriteLine(" Copyright 2019. Author: Charles A.");            
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            RunApp r = new RunApp();
            r.Run();
            Console.Write("\n\n\t Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
