using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace SuperSuper.Basics
{
    public static class Spiral
    {
        public static Curve GetSpiral(Plane center, double radius, double turns,  int density)
        {
            if (density < turns * 2) return null;

            var pts = new List<Point3d>() { center.Origin };
            var degreesMax = turns * Math.PI;
            var stepRotation = degreesMax / density;
            var stepOffset = radius / density;

            for (int i = 1; i < density; i++)
            {
                var thisRotation = i * stepRotation;
                var thisOffset = i * stepOffset;

                var nextPt = new Point3d(center.Origin);
                nextPt += center.YAxis * thisOffset; //move
                var xForm = Transform.Rotation(thisRotation, center.Normal, center.Origin); //rotate
                nextPt.Transform(xForm);
                pts.Add(nextPt);
            }

            return Curve.CreateInterpolatedCurve(pts, 3);
        }
    }
}
