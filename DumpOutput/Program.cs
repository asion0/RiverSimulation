using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            if(!System.IO.File.Exists(args[0]))
            {
                Console.WriteLine("File not found!");
                return;
            }
            int s = 100;
            if (args.Length >= 2)
            {
                try
                {
                    s = Convert.ToInt32(args[1]);
                }
                catch
                {
                    s = 100;
                }
            }


            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(args[0]);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                System.Threading.Thread.Sleep(s);
                counter++;
            }

        }
    }
}
