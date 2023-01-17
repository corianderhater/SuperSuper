using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SuperSuper.Audio
{
    public class ChladniPlateComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ChladniPlateComponent class.
        /// </summary>
        public ChladniPlateComponent()
          : base("ChladniPlateComponent", "Nickname",
              "Description",
              "SuperSuper", "Audio")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Hz", "Hz", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Magnitude", "M", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Scale", "S", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Pts", "p", "", GH_ParamAccess.list);
            pManager.AddMeshParameter("Mesh", "M", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var pts = new List<Point3d>();
            var mesh = new Mesh();
            int width = 100;
            int height = 100;
            int hz = 50;
            double mag = 0;
            double scale = 0;
            if(!DA.GetData(0, ref hz)) return;
            if(!DA.GetData(1, ref mag)) return;
            if(!DA.GetData(2, ref scale)) return;

            for (double x = 0; x < width; x++)
            {
                double xN = ((x - width / 2)) / scale;
                for (double y = 0; y < height; y++)
                {
                    double yN = (y - height / 2) / scale;
                    double h = Math.Sin(x) + Math.Sin(xN * yN *yN) * Math.Cos(yN * yN) * mag;

                    pts.Add(new Point3d(x, y, h));
                }
            }


            DA.SetDataList(0, pts);
            DA.SetData(1, mesh);
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
            get { return new Guid("D9A34B6E-22E6-48BB-A58A-368DDA7E63DC"); }
        }
    }
}