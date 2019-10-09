using System.Drawing;

namespace hospitalAPI.ColorTransformation
{
    /// <summary>
    /// Class with algorithm to change greyscale to colorscale image
    /// </summary>
    public class ColorChanger
    {
        private static readonly Bitmap scale = new Bitmap(hospitalAPI.Properties.Resources.pallette);
        private Bitmap imageToConvert;
        private Bitmap coloredImage;
        private string filePath;
        

        public ColorChanger(string filePath)
        {
            imageToConvert = new Bitmap(filePath);
            this.filePath = filePath;
        }
        /// <summary>
        ///
        /// </summary>
        public void ColorImage()
        {
            //imageToGreyScale();
            this.coloredImage = imageToConvert;
            for(int row = 0; row < coloredImage.Height; row++)
            {
                for (int col = 0; col < coloredImage.Width; col++)
                {
                    Color pixel = coloredImage.GetPixel(col, row);
                    //if ((pixel.R != pixel.B && pixel.B != pixel.G) || pixel.A != 255)
                    //{
                    //    int average = ((int)pixel.R + (int)pixel.G + (int)pixel.B) / 3;
                    //    pixel = Color.FromArgb(255, average, average, average);
                    //}
                    coloredImage.SetPixel(col, row, ColorPixel(pixel));
                };
            };
        }

        public Color ColorPixel(Color greyScalePixel)
        {
            Color rainbowScaleColor = new Color();
            Color tempColor = new Color();
            int ScaleHeight = scale.Height;
                for(int col = 0; col < scale.Width; col++)
                {
                    tempColor = scale.GetPixel(col, 1);
                int white = greyScalePixel.ToArgb();
                if(greyScalePixel.Name == "ffffffff")
                {
                    return rainbowScaleColor = scale.GetPixel(454, 0);
                }

                else if(tempColor.ToArgb() >= greyScalePixel.ToArgb())
                    {
                        return rainbowScaleColor = scale.GetPixel(col, 0);
                    }
                };

            return greyScalePixel;
        }

        public void SaveColoredImage(string savePath)
        {
            coloredImage.Save(savePath);
        }

        public void imageToGreyScale()
        {
            for (int row = 0; row < imageToConvert.Height; row++)
            {
                for (int col = 0; col < imageToConvert.Width; col++)
                {
                    Color pixel = imageToConvert.GetPixel(col, row);
                    if((pixel.R != pixel.B && pixel.B != pixel.G)||pixel.A != 255)
                    {
                        int average = ((int)pixel.R + (int)pixel.G + (int)pixel.B) / 3;
                        Color transfomedPixel = Color.FromArgb(255, average, average, average);
                        imageToConvert.SetPixel(col, row, transfomedPixel);
                    }
                };
            };
        }
    }
}