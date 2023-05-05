using System;

public class TangentialCircle
{
    public double[] Center { get; private set; }
    public double Radius { get; private set; }

    public TangentialCircle(double[] pointP, double[] lineL)
    {
        // Step 1: Find the distance between the point P and the line L
        double d = DistancePointToLine(pointP, lineL);

        // Step 2: Find the equation of the perpendicular line passing through point P
        double[] perpendicularLine = PerpendicularLine(pointP, lineL);

        // Step 3: Find the radius of the circle
        double r = d;

        // Step 4: Find the center of the circle
        double[] center = new double[2];
        center[0] = perpendicularLine[0];
        center[1] = perpendicularLine[1];

        // Set the properties of the TangentialCircle object
        Center = center;
        Radius = r;
    }

    private double DistancePointToLine(double[] point, double[] line)
    {
        double x = point[0];
        double y = point[1];
        double a = line[0];
        double b = line[1];
        double c = line[2];
        double numerator = Math.Abs(a * x + b * y + c);
        double denominator = Math.Sqrt(a * a + b * b);
        double distance = numerator / denominator;
        return distance;
    }

    private double[] PerpendicularLine(double[] point, double[] line)
    {
        double[] perpendicularLine = new double[2];
        double xp = point[0];
        double yp = point[1];
        double a = line[0];
        double b = line[1];
        double slopeL = -a / b;
        double yinterceptL = -line[2] / b;
        perpendicularLine[0] = (yp - yinterceptL + xp / slopeL) / (slopeL + 1 / slopeL);
        perpendicularLine[1] = slopeL * perpendicularLine[0] + yinterceptL;
        return perpendicularLine;
    }
}