using System;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numFile = 4400;
            var allData = new byte[numFile * 128];
            var rectsFor8 = new Rectangle[] { new Rectangle(2, 0, 6, 2), new Rectangle(7, 1, 2, 6), new Rectangle(7, 8, 2, 6), new Rectangle(2, 13, 6, 2),
                new Rectangle(1, 8, 2, 6), new Rectangle(1, 1, 2, 6), new Rectangle(2, 7, 6, 2), new Rectangle(8, 12, 2, 3) };
            var rects = new Rectangle[8*16*8];
            for (int i = 0; i < 8; ++i) for (int j = 0; j < 16; ++j) for (int k = 0; k < 8; ++k)
                        rects[i * 16 * 8 + j * 8 + k] = new Rectangle(10 * j + rectsFor8[k].X, 15 * i + rectsFor8[k].Y, rectsFor8[k].Width, rectsFor8[k].Height);
            for (int n = 0; n < numFile; ++n) {
                var bits = new bool[rects.Length];
                var bitmap = new Bitmap(Image.FromFile("Movie\\" + n.ToString() + ".bmp"));
                for (int k = 0; k < bits.Length; ++k)
                {
                    int numWhite = 0;
                    for (int i = rects[k].X; i < rects[k].X + rects[k].Width; ++i) for (int j = rects[k].Y; j < rects[k].Y + rects[k].Height; ++j)
                            if (bitmap.GetPixel(i, j).ToArgb() == Color.White.ToArgb()) numWhite++;
                    if (numWhite > rects[k].Width * rects[k].Height / 2) bits[k] = true;
                }
                var data = new byte[128];
                for (int k = 0; k < 4; ++k) for (int m = 0; m < 2; ++m) for (int j = 0; j < 8; ++j) for (int l = 0; l < 2; ++l) for (int i = 0; i < 8; ++i)
                                    if (bits[k * 8 * 16 * 2 + l * 16 * 8 + m * 8 * 8 + i * 8 + j]) data[k * 16 * 2 + m * 2 * 8 + 2 * j + l] |= (byte)(1 << i);
                for (int i = 0; i < data.Length; ++i) allData[n * 128 + i] = data[i];
            }
            var fs = new System.IO.FileStream("Movie.dat", System.IO.FileMode.Create);
            fs.Write(allData, 0, allData.Length);
            fs.Close();
        }
    }
}
