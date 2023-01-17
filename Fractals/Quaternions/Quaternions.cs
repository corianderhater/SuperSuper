using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SuperSuper.Fractals.Quaternions
{
    public class Quaternions
    {
        //z = a + bi + cj + dk      where a, b, c, and d are real numbers; and i, j, and k are the basic quaternions.
        //https://www.cs.cmu.edu/~kmcrane/Projects/QuaternionJulia/paper.pdf
        public static Vector4 QuaterionMult( Vector4 q1, Vector4 q2)
        {
            var r = new Vector4(new Vector3(), 2);
       
            var q1YZW = new Vector3(q1.Y, q1.Z, q1.W);
            var q2YZW = new Vector3(q2.Y, q2.Z, q2.W);

            var rYZW = q1.X * q2YZW + q2.X * q1YZW + Vector3.Cross(q1YZW, q2YZW);

            r.X = q1.X * q2.X - Vector3.Dot(q1YZW, q2YZW);
            r.Y = rYZW.X; //X = Y
            r.Z = rYZW.Y; //Y = Z
            r.W = rYZW.Z; //Z = W

            return r;

        }

        public static Vector4 QuatSq(Vector4 q)
        {
            var r = new Vector4(new Vector3(), 2);

            var qYZW = new Vector3(q.Y, q.Z, q.W);
            var rYZW = q.X * qYZW + q.X * qYZW + Vector3.Cross(qYZW, qYZW);

            r.X = q.X * q.X - Vector3.Dot(qYZW, qYZW);
            r.Y = rYZW.X; //X = Y
            r.Z = rYZW.Y; //Y = Z
            r.W = rYZW.Z; //Z = W

            return r;
        }

        public static int ComputeQuaterionStability( Vector4 z, Vector4 c, int maxIterations)
        {
            int counter = 0;
            for (int i = 0; i < maxIterations; i++)
            {
                z = QuatSq(z) + c;
                if (Vector4.Dot(z, z) > 3)
                    break;

                counter++;
            }

            return counter;
        }

        public static int ComputeQuaterionStability(Vector4 z, Vector4 qp, Vector4 c, int maxIterations)
        {
            int counter = 0;
            for (int i = 0; i < maxIterations; i++)
            {
                qp = 2 * QuaterionMult(z, qp);
                z = QuatSq(z) + c;

                if (Vector4.Dot(z, z) > 3)
                    break;

                counter++;
            }

            return counter;
        }

    }
}
