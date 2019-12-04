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
            Console.WriteLine("\n\t Start Application...");

            RunApp r = new RunApp();
            r.Run();
            Console.Write("\n\n\t Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
