using System;
using System.Collections.Generic;
using System.Numerics;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SuperSuper.Basics
{
    public class Vector4Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Vector4Component class.
        /// </summary>
        public Vector4Component()
          : base("Vector4", "Vec4",
              "Constructs Vector4",
              "SuperSuper", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x", "x", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("y", "y", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("z", "z", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("w", "w", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Vector4", "Vec4", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double w = 0;
            if (!DA.GetData(0, ref x)) return;
            if (!DA.GetData(1, ref y)) return;
            if (!DA.GetData(2, ref z)) return;
            if (!DA.GetData(3, ref w)) return;

            DA.SetData(0, new Vector4((float)x, (float)y, (float)z, (float)w));
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
            get { return new Guid("EBC4A1E2-519F-4CBF-9E2C-93F85854F9A5"); }
        }
    }
}