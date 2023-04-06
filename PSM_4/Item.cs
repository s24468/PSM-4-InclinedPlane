using System;

namespace PSM_4
{
    public class Item
    {
        public double Radius { get; private set; }
        public double Mass { get; private set; }
        public double I { get; set; } // moment bezwładności
        public double V { get; set; } //prędkość obiektu

        public double Beta { get; set; } // kąt obrotu
        public double A { get; set; } //przyspieszenie liniowe obiektu


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
            Beta = 0;
            Angle = new Angle(x, y+1);
        }

        public Angle Angle;


    }

    public class Angle
    {
        public double W { get; set; } //prędkość kątowa
        public double E { get; set; } //przyspieszenie kątowe

        public double XAngle; //wartość wpółrzędnej X kata
        public double YAngle; //wartość wpółrzędnej Y kata

        public double XAngleRounded => Math.Round(XAngle, 7);

        public double YAngleRounded => Math.Round(YAngle, 7);

        public Angle(double xAngle, double yAngle)
        {
            XAngle = xAngle;
            YAngle = yAngle;
            W = 0;
            E = 0;
        }
    }
}