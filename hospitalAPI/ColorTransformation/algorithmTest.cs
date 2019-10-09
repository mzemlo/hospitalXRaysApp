using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalAPI.ColorTransformation
{
    public class algorithmTest
    {
            static void Main(string[] args)
            {
                string filePath = @"C:\Users\kmpjo\Desktop\test\bone-xray-hands.jpg";
            ColorChanger algorithm = new ColorChanger(filePath);
            algorithm.ColorImage();

            algorithm.SaveColoredImage(@"C:\Users\kmpjo\Desktop\test\bone-xray-handsInColor.jpg");
            Console.ReadKey();
            }
        
    }
}