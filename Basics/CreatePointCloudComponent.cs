using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace SuperSuper.Basics
{
    public class CreatePointCloudComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CreatePointCloudComponent class.
        /// </summary>
        public CreatePointCloudComponent()
          : base("CreatePointCloud", "PTCloud",
              "Description",
              "SuperSuper", "Voxels")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Pts", "Pts", "", GH_ParamAccess.list);
            pManager.AddColourParameter("Colors", "C", "", GH_ParamAccess.list);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("PtCloud", "PtCloud", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            var pts = new List<Point3d>();
            var colors = new List<System.Drawing.Color>();
            if (!DA.GetDataList(0, pts)) return;
            if (!DA.GetDataList(1, colors)) return;
            if (pts.Count != colors.Count) return;
            PointCloud pc = new PointCloud();
            var n = new Vector3d(0, 0, 1);
            for (int i = 0; i < pts.Count; i++)
            {
                pc.Add(pts[i], n, colors[i]);
            }

            DA.SetData(0, pc);

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
            get { return new Guid("40A40837-EAEB-447F-A087-36B048F30CF0"); }
        }
    }
}