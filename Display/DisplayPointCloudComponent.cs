using Grasshopper.Kernel;
using Rhino.Geometry;
using System;

namespace SuperSuper.Display
{
    public class DisplayPointCloudComponent : GH_Component
    {
        public DisplayPointCloud Conduit { get; private set; }
        /// <summary>
        /// Initializes a new instance of the DisplayPointCloudComponent class.
        /// </summary>

        public DisplayPointCloudComponent()
          : base("DisplayPointCloud", "DPC",
              "Description",
              "SuperSuper", "Display")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("PointCloud", "PC", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Size", "S", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>gra
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            PointCloud pc = new PointCloud();
            double size = 0;
            bool isOn = false;

            if (!DA.GetData(0, ref pc)) return;
            if (!DA.GetData(1, ref size)) return;
            if (!DA.GetData(2, ref isOn)) return;

            Conduit = DisplayPointCloud.GetInstance();

            this.Conduit.PC = pc;
            this.Conduit.Size = (float)size;
            this.Conduit.Enabled = isOn;

            Rhino.RhinoDoc.ActiveDoc.Views.Redraw();

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
            get { return new Guid("27B3A17B-BCF8-4841-86F0-DC0E440028BC"); }
        }
    }
}