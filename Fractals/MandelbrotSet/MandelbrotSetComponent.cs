using System;
using System.Collections.Generic;
using SuperSuper.Fractals.MandelbrotSet;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SuperSuper.Fractals.MandelbrotSet
{
    public class MandelbrotSetComponent : GH_Component
    {
        public MandelbrotSetComponent()
          : base("MandelbrotSet", "Nickname",
              "Description",
              "SuperSuper", "Fractals")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "R", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Density", "D", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "", GH_ParamAccess.list);
            pManager.AddNumberParameter("StabilityRate", "SR", "", GH_ParamAccess.list);
            
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Rectangle3d canvas = new Rectangle3d(Plane.WorldXY, 1,1);
            double density = 1;
            if (!DA.GetData(0, ref canvas)) return;
            if (!DA.GetData(1, ref density)) return;

            int width = (int)(canvas.Width / density);
            int height = (int)(canvas.Height / density);
            var values = MandelbrotSetEngine.ComputeMandelbrotSetValues(new System.Drawing.Size(width, height));
            var flatValues = new List<int>();
            foreach (var num in values) flatValues.Add(num);
            var pts = new List<Point3d>();

            var xInt = canvas.Width / width;
            var yInt = canvas.Height / height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pts.Add(new Point3d(canvas.Plane.OriginX + x * xInt, canvas.Plane.OriginY + y * yInt, 0));
                }
            }

            DA.SetDataList(0, pts);
            DA.SetDataList(1, flatValues);

        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid => new Guid("A6D64F0A-AB74-49BE-91DF-039D1EBD43DA");
    }
}