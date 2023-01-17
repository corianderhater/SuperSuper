using System;

namespace SuperSuper.Fractals
{

    public class Complex
    {
        public double A; //real
        public double B; //imaginary

        public Complex(double a, double b)
        {
            A = a;
            B = b;
        }

        public void Square()
        {
            double temp = (A * A) - (B * B);
            B = 2.0 * A * B;
            A = temp;
        }

        public double Magnitude()
        {
            return Math.Sqrt((A * A) + (B * B));
        }

        public void Add(Complex c)
        {
            A += c.A;
            B += c.B;
        }
    }
}
