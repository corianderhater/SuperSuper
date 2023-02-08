using System;
using System.Collections.Generic;
using System.Numerics;
using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSuper.Fractals.Quaternions
{
    public class QuaterionFractalComponent : GH_Component
    {

        public QuaterionFractalComponent()
          : base("QuaterionFractal", "QF",
              "Description",
              "SuperSuper", "Fractals")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Range", "R", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Density", "D", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Scale", "S", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("MinIterationsFilter", "MinIF", "", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("MaxIterationsFilter", "MaxIF", "", GH_ParamAccess.item, 100);
            pManager.AddIntegerParameter("MaxIterations", "MaxI", "", GH_ParamAccess.item, 100);
            pManager.AddGenericParameter("C_Vector4", "C_Vec", "Vector4 C", GH_ParamAccess.item);
            pManager.AddNumberParameter("Wz", "Wz", "W part of Vector4 Z", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Value", "V", "", GH_ParamAccess.list);
            pManager.AddPointParameter("Points", "Pt", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("PointCloud", "PC", "", GH_ParamAccess.item);
            pManager.AddTextParameter("Log", "Log", "", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var totalTimestamp = DateTime.Now.Ticks;
            int range = 0;
            int density = 0;
            double scale = 0.0;
            int minIterationsFilter = 0;
            int maxIterationsFilter = 0;
            int maxIterations = 0;
            Vector4 cV = new Vector4();
            double Wz = 0;

            if (!DA.GetData(0, ref range)) return;
            if (!DA.GetData(1, ref density)) return;
            if (!DA.GetData(2, ref scale)) return;
            if (!DA.GetData(3, ref minIterationsFilter)) return;
            if (!DA.GetData(4, ref maxIterationsFilter)) return;
            if (!DA.GetData(5, ref maxIterations)) return;
            if (!DA.GetData(6, ref cV)) return;
            if (!DA.GetData(7, ref Wz)) return;

            var result = new List<(Point3d Point, int StabilityValue)>();
            var pc = new PointCloud();
            var interval = range / (float)density;

            for (double x = -range; x < range; x += interval)
            {
                for (double y = -range; y < range; y += interval)
                {
                    for (double z = -range; z < range; z += interval)
                    {
                        var zV = new Vector4((float)x, (float)y, (float)z, (float)Wz);
                        
                        var value = Quaternions.ComputeQuaterionStability(zV, cV, maxIterations);
                        if (value >= minIterationsFilter && value <= maxIterationsFilter)
                        {
                            var pt = new Point3d(x, y, z);
                            result.Add((pt, value));
                            var nVal = value / (float)maxIterations;
                            int rVal = (int)(1/nVal* 255);
/*                            int r, g, b;
                            if(nVal < 0.09)
                            {
                                r = 255;
                                g = 0;
                                b = 0;
                            }
                            else if(rVal < 0.9)
                            {
                                r = 0;
                                g = 255;
                                b = 0;
                            }
                            else
                            {
                                r = 0;
                                g = 0;
                                b = 255;
                            }*/
                            //pc.Add(pt, System.Drawing.Color(255, r, g, b));
                            pc.Add(pt, System.Drawing.Color.FromArgb(rVal));
                        }
                    }
                }
            }

            var log = 
                "cV4: " + cV.ToString() + 
                " Wz: " + Wz.ToString() + 
                " maxIt: " + maxIterations.ToString() + 
                " minFilter: " + minIterationsFilter.ToString() + 
                " maxFilter: " + maxIterationsFilter.ToString();

            DA.SetDataList(0, result.Select(o => o.StabilityValue));
            DA.SetDataList(1, result.Select(o => o.Point));
            DA.SetData(2, pc);
            DA.SetData(3, log);

            RhinoApp.WriteLine("Quaterion fractal processed in: {0} ms", (DateTime.Now.Ticks - totalTimestamp) / 10000);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("F0C73CDB-7E20-4C3C-9467-DCF45E7343B4"); }
        }
    }
}