using System;

namespace PSM_4
{
    public class Item
    {
        public double Radius { get; private set; }
        public double Mass { get; private set; }
        public double I { get; set; }

        public double X;
        public double Y;

        public double XRounded => Math.Round(X, 7);

        public double YRounded => Math.Round(Y, 7);


        public Item(double radius, double mass, double x, double y)
        {
            I = 0;
            Radius = radius;
            Mass = mass;
            X = x;
            Y = y;
        }
    }
}