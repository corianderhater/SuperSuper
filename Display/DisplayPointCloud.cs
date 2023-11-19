using Rhino.Display;
using Rhino.Geometry;

namespace SuperSuper.Display
{

    public sealed class DisplayPointCloud : DisplayConduit
    {
        private static DisplayPointCloud _instance;
        public PointCloud PC { get; set; }
        public float Size { get; set; }

        public static DisplayPointCloud GetInstance()
        {
            _instance ??= new DisplayPointCloud();
            return _instance;
        }

        public void Preview(PointCloud pc, float size)
        {
            PC = pc;
            Size = size;
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