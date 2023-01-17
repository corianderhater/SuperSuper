using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SuperSuper.Basics
{
    public class SpiralComponent : GH_Component
    {

        public SpiralComponent()
          : base("Spiral", "Spiral",
              "Description",
              "SuperSuper", "Basics")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Center", "C", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "R", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Turns", "T", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Density", "D", "", GH_ParamAccess.item);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Spiral", "S", "", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Plane center = Plane.Unset;
            double radius = 0;
            double turns = 0;
            int density = 1;

            if (!DA.GetData(0, ref center)) return;
            if (!DA.GetData(1, ref radius)) return;
            if (!DA.GetData(2, ref turns)) return;
            if (!DA.GetData(3, ref density)) return;

            var spiral = Spiral.GetSpiral(center, radius, turns, density);

            DA.SetData(0, spiral);
        }


        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("A9BBF637-6F7C-472B-B657-597A0274ED0E");
    }
}