using System;

namespace PSM_4
{
    public class Item
    {
        public double Radius { get; private set; }
        public double Mass { get; private set; }
        public double I { get; set; } // moment bezwładności

        public double V { get; set; } //prędkość obiektu

        public double A { get; set; } //przyspieszenie liniowe obiektu
        
        public double W { get; set; } //prędkość kątowa
        public double E { get; set; } //przyspieszenie kątowe


        public double X; //wartość wpółrzędnej X
        public double Y; //wartość wpółrzędnej Y

        public double XRounded => Math.Round(X, 7);

        public double YRounded => Math.Round(Y, 7);


        public Item(double radius, double mass, double x, double y)
        {
            I = 0;
            Radius = radius;
            Mass = mass;
            X = x;
            Y = y;
            A = 0;
            V = 0;
            W = 0;
            E = 0;
        }
    }
}