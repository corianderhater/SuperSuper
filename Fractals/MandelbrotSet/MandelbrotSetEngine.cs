using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSuper.Fractals.MandelbrotSet
{
    public static class MandelbrotSetEngine
    {
        public static Bitmap ComputeMandelbrotSet(Size size)
        {
            Bitmap bm = new Bitmap(size.Width, size.Height);
            
            for (int x = 0; x < size.Width; x++)
            {
                double a = (double)(x - size.Width / 2) / (double)(size.Width / 4);
                for (int y = 0; y < size.Height; y++)
                {
                    //normalize values

                    double b = (double)(y - size.Height / 2) / (double)(size.Height / 4);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);
                    int it = 0;

                    for (int i = 0; i < 100; i++)
                    {
                        it++;
                        z.Square();
                        z.Add(c);
                        if (z.Magnitude() > 2.0) break;
                    }

                    //bm.SetPixel(x, y, it < 100 ? Color.Black : Color.White);
                    bm.SetPixel(x, y, it < 100 ? Color.FromArgb((it * 255) / 100, (it * 30) / 100, (it * 255) / 100) : Color.Black);
                }
            }
            
            return bm;
        }

        public static int[,] ComputeMandelbrotSetValues(Size size)
        {
            var result = new int[size.Width, size.Height];

            for (int x = 0; x < size.Width; x++)
            {
                double a = (double)(x - size.Width / 2) / (double)(size.Width / 4);
                for (int y = 0; y < size.Height; y++)
                {
                    //normalize values

                    double b = (double)(y - size.Height / 2) / (double)(size.Height / 4);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);
                    int it = 0;
                    double zMag = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        it++;
                        z.Square();
                        z.Add(c);
                        zMag = z.Magnitude();
                        if (zMag > 2.0) break;
                    }

                    result[x, y] = it;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns count of iterations until the number is stable within the interations range.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public static int ComputeMandelbrotUnit(double x, double y, int iterations)
        {

            Complex c = new Complex(x, y);
            Complex z = new Complex(0, 0);
            int it = 0;
            
            for (int i = 0; i < iterations; i++)
            {
                it++;
                z.Square();
                z.Add(c);
                if (z.Magnitude() > 2.0) break;
            }

            return it;
        }

    }
}
