using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace SuperSuper
{
    public class SuperSuperInfo : GH_AssemblyInfo
    {
        public override string Name => "SuperSuper";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("F73B1B4A-6A93-43E4-81FC-3E3AE4154883");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}