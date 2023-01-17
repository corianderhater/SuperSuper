using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace SuperSuper.Basics
{
    public class Voxels
    {

        public static PointCloud GetPointCloudCube(Point3d center, int sizeX, int sizeY, int sizeZ, double offset)
        {
            var pt = center + new Vector3d(-(sizeX * offset) / 2, -(sizeY * offset) / 2, -(sizeZ * offset) / 2);
            var ptCloud = new PointCloud();
            for (int x = 0; x < sizeX; x++)
            {
                for (int y=0; y < sizeY; y++)
                {
                    for (int z = 0; z < sizeZ; z++)
                    {
                        var newPt = pt + new Vector3d(x * offset, y * offset, z * offset);
                        ptCloud.Add(newPt);

                    }
                }
            }
            return ptCloud;
        }

        public static PointCloud GetPointCloudCube(List<Point3d> pts, List<System.Drawing.Color> colors)
        {
            if (pts.Count != colors.Count) return null;
            var ptCloud = new PointCloud();
            for (int x = 0; x < pts.Count; x++)
            {
                ptCloud.Add(pts[x], colors[x]);
            }
            return ptCloud;
        }
    }
}
