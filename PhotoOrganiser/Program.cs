using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganiser
{
    class Program
    {
        static void Main(string[] args)
        {
            IAttributeProvidor attrHandler = new DotNet3ImageAttributeProvidor();

            var sw = Stopwatch.StartNew();
            var attributes = attrHandler.GetAttributes(@"C:\Users\hatha\Google Drive\2016-05-03 iphone\100APPLE\IMG_0182.jpg");
            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }
    }
}
