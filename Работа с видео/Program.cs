using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Работа_с_видео
{
    class Program
    {
        private static char[][] masChar;

        static void Main(string[] args)
        {
            Console.ReadKey();

            Thread th = new Thread(WriteVedio);
            th.Start();

            Console.ReadKey();
        }

        /// <summary>
        /// Show Video
        /// </summary>
        private static void WriteVedio()
        {
            for (int z = 1; z <= 6487; z++)
            {

                string fileName = z.ToString();

                if (fileName.Length == 1)
                    fileName = "0000" + fileName;
                if(fileName.Length == 2)
                    fileName = "000" + fileName;
                if (fileName.Length == 3)
                    fileName = "00" + fileName;
                if (fileName.Length == 4)
                    fileName = "0" + fileName;

                //Get frame
                Bitmap bitmap = (Bitmap)Image.FromFile($"MainVideo\\scene{fileName}.png");
                masChar = new char[bitmap.Height][];
                bitmap = BitmapCompress(bitmap);

                //Translate frame on String
                for (int i = 0; i < bitmap.Height; i++)
                {
                    char[] mas = new char[bitmap.Width];
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        var cl = bitmap.GetPixel(j, i);

                        if (cl == Color.FromArgb(255,253, 253, 253))
                        {
                            mas[j] = '1';
                        }
                        else
                        {
                            mas[j] = '0';
                        }
                    }
                    masChar[i] = mas;
                }


                //Show Frame in Console
                foreach (var item in masChar)
                {
                    if (item == null)
                        break;
                    Console.WriteLine(item);
                }


                Console.SetCursorPosition(0, 0);
            }

        }

        /// <summary>
        /// Resize Bitmap
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Bitmap BitmapCompress(Bitmap value)
        {
            var maxWidh = 240;
            var newHeigh = value.Height / 1.5 * maxWidh / value.Width;
            if (value.Width > maxWidh || value.Height > newHeigh)
                value = new Bitmap(value, new Size(maxWidh, (int)newHeigh));

            return value;
        }
    }
}
