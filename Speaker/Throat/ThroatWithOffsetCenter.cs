using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SuperSuper.Speaker.Throat
{
    public class ThroatWithOffsetCenter : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ThroatWithOffsetCenter class.
        /// </summary>
        public ThroatWithOffsetCenter()
          : base("ThroatWithOffsetCenter", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //x = [a * c + sqrt(c ^ 2 + 1) * b] / (c ^ 2 + 1) or x = [a * c - sqrt(c ^ 2 + 1) * b] / (c ^ 2 + 1)
            //x = xp, a = T, b = D, c = tga
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
            get { return new Guid("DBA8CFCC-5611-4B78-A9A6-54A421E1FDFB"); }
        }
    }
}