using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospitalAPI.ColorTransformation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i<20; i++)
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();
                string filePath = @"C:\Users\Feste\Desktop\test\testPredkosci.jpg";
                ColorChanger algorithm = new ColorChanger(filePath);
                Console.WriteLine("Start run {0}", i);
                algorithm.ColorImage();
                Console.WriteLine("Saving...");
                algorithm.SaveColoredImage(@"C:\Users\Feste\Desktop\test\coloredTestPredkosci.jpg");
                watch.Stop();
                var elapsedSeconds = watch.ElapsedMilliseconds / 1000;
                Console.WriteLine("End run {0} in {1} seconds.", i, elapsedSeconds);

            }

            Console.ReadKey();
        }
    }
}
