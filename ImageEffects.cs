using UnityEngine;
using System.Collections;
using System.Drawing;
using System;
using System.Drawing.Imaging;

public class ImageEffects : MonoBehaviour {
    Material mat;
    public static string fileName = @"FilePath";
    Int32 size = 100;

    void Start()
    {
        Bitmap bitmap = new Bitmap(fileName);

        Bitmap bm = Pixelate(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 110);

        EncoderParameters encoderParameters = new EncoderParameters(1);
        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
        bm.Save(@"FilePath", GetEncoder(ImageFormat.Jpeg), encoderParameters);
        // Texture imagee = Texture.file(@"C:\Users\trep\Documents\Development\Unity\Tools\Assets\output.jpg");
        //mat.mainTexture = imagee;
    }

    public static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }

        return null;
    }

    private static Bitmap Pixelate(Bitmap image, Rectangle rectangle, Int32 pixelateSize)
    {
        Bitmap pixelated = new System.Drawing.Bitmap(image.Width, image.Height);

        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(pixelated))
            graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

        for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx += pixelateSize)
        {
            for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy += pixelateSize)
            {
                Int32 offsetX = pixelateSize / 2;
                Int32 offsetY = pixelateSize / 2;

                // make sure that the offset is within the boundry of the image
                while (xx + offsetX >= image.Width) offsetX--;
                while (yy + offsetY >= image.Height) offsetY--;

                // get the pixel color in the center of the soon to be pixelated area
                System.Drawing.Color pixel = pixelated.GetPixel(xx + offsetX, yy + offsetY);

                // for each pixel in the pixelate size, set it to the center color
                for (Int32 x = xx; x < xx + pixelateSize && x < image.Width; x++)
                    for (Int32 y = yy; y < yy + pixelateSize && y < image.Height; y++)
                        pixelated.SetPixel(x, y, pixel);
            }
        }

        return pixelated;
    }
}
