using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Input.Custom;

namespace SuperSuper.Speaker.Throat
{
    public class ThroatWithOffsetCenter : GH_Component
    {

        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("DBA8CFCC-5611-4B78-A9A6-54A421E1FDFB");

        public ThroatWithOffsetCenter()
          : base("ThroatWithOffsetCenter", "Nickname",
              "Description",
              "SuperSuper", "Speakers")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Center", "C", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("CenterOffset", "CO", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("ThroatRadius", "TR", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("HorizontalDispersionAngle", "HA", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("VerticalDispersionAngle", "VA", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Resolution", "R", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("C", "C", "", GH_ParamAccess.list);
            pManager.AddBrepParameter("B", "B", "", GH_ParamAccess.list);
            pManager.AddNumberParameter("xc", "xc", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Plane center = Plane.Unset;
            double centerOffset = 0.0;
            double throatRadius = 0.0;
            double hAngle = 0.0;
            double vAngle = 0.0;
            int res = 0;

            if (!DA.GetData(0, ref center)) { return; }
            if (!DA.GetData(1, ref centerOffset)) { return; }
            if (!DA.GetData(2, ref throatRadius)) { return; }
            if (!DA.GetData(3, ref hAngle)) { return; }
            if (!DA.GetData(4, ref vAngle)) { return; }
            if (!DA.GetData(5, ref res)) { return; }



            /*            var circles = new List<Curve>();
                        var angle = 90 - hAngle;
                        var x = GetCircleXPosition(Math.PI/2 - angle, centerOffset, throatRadius);
                        var circleCenter = new Point3d(x, -centerOffset, 0);
                        var line = new Line(center.Origin, center.Origin + new Vector3d(1000, 1000 * Math.Tan(angle), 0));
                        var param = line.ClosestParameter(circleCenter);
                        var PT = new Point3d(center.Origin.X + throatRadius, center.OriginY, center.OriginZ);
                        var r = PT.DistanceTo(circleCenter);

                        var circlePlane = new Plane(circleCenter, Vector3d.ZAxis);

                        var circle = new Circle(circlePlane, r);

                        DA.SetData(0, circle);*/
            var radians = Rhino.RhinoMath.ToRadians(hAngle);
            var slope = Math.Tan(hAngle);
            var xc = GetCircleX(slope, centerOffset, throatRadius);
            var circle = GetCircle(slope, new Point3d(throatRadius, 0, 0), centerOffset);
            var curves = new List<Curve>() { circle.ToNurbsCurve() };
            DA.SetDataList(0, curves);
            DA.SetData(2, xc);
        }

        private double GetCircleXPosition(double angle, double centerOffset, double throatRadius)
        {
            //x = [a * c + sqrt(c ^ 2 + 1) * b] / (c ^ 2 + 1) or x = [a * c - sqrt(c ^ 2 + 1) * b] / (c ^ 2 + 1)
            //x = xp, a = T, b = D, c = tga

            var c = Math.Tan(Rhino.RhinoMath.ToRadians(angle));
            var b = -centerOffset;
            var a = throatRadius;
            //var x = (a * c + Math.Sqrt(Math.Pow(c,2) + 1) * b) / (Math.Pow(c, 2) + 1);
            var x = (a * c - Math.Sqrt(Math.Pow(c,2) + 1) * b) / (Math.Pow(c, 2) + 1);
            return x;
        }

        public static double GetCircleX(double angle, double offset, double throatRadius)
        {
            double numerator = offset * angle + Math.Sqrt(Math.Pow(offset * angle, 2) - angle * angle * (angle * angle + 1) * (2 * throatRadius * (1 + angle * angle) - (1 + angle * angle) * throatRadius * throatRadius));
            double denominator = 1 + angle * angle;
            double x = numerator / denominator;
            return x;
        }


        private Circle GetCircle(double tan, Point3d pT, double circleOffset)
        {
            var pt = new double[] { pT.X, pT.Y};
            var line = new double[3] { tan, 1, 0 };
            var c = new TangentialCircle(pt, line);
            return new Circle(new Point3d(c.Center[0], c.Center[1], 0), c.Radius);
        }
    }
}
