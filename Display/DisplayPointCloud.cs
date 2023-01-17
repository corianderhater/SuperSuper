using Rhino.Geometry;
using Rhino.Display;

namespace SuperSuper.Display
{

    public class DisplayPointCloud : DisplayConduit
    {

        public PointCloud PC { get; set; }
        public float Size { get; set; }
        public DisplayPointCloud()
        {
            PC = new PointCloud
            {
                { new Point3d(0, 0, 0), System.Drawing.Color.Red }
            };
            Size = (float)22.12;
        }

        protected override void PreDrawObjects(DrawEventArgs e)
        {
            base.PreDrawObjects(e);
            e.Display.DrawPointCloud(PC, Size);
        }

        protected override void CalculateBoundingBox(CalculateBoundingBoxEventArgs e)
        {
            base.CalculateBoundingBox(e);
            e.IncludeBoundingBox(PC.GetBoundingBox(true));
            
        }
    }
}