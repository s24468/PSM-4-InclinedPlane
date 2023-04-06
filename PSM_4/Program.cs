using System;
using System.Collections.Generic;
using PSM_4;

class Program
{
    private const double Alpha = 30 * (Math.PI / 180); // Kąt nachylenia równi w radianach (30 stopni)
    private const double G = 9.81; // Przyspieszenie ziemskie (m/s^2)

    static void Main(string[] args)
    {
        var kula = new Item(1, 1, 0, 20);

        double totalTime = 5.0; // Czas symulacji (s)
        double timeStep = 0.05; // Krok czasowy (s)

        // Moment bezwładności dla kuli 
        kula.I = 0.4 * (kula.Mass * Math.Pow(kula.Radius, 2));

        // Symulacja dla kuli
        kula.A = G * Math.Sin(Alpha) /
                 (1 + (kula.I / (kula.Mass * Math.Pow(kula.Radius, 2)))); // przyspieszenie liniowe
        DoSimulation(totalTime, timeStep, kula, "ball");
        
        
        //- - - - - - - - - - - - - - SYMULACJA DLA SFERY - - - - - - - - - - - - - -
        
        // Console.WriteLine();
        // // Symulacja dla sfery
        // var sphere = new Item(2, 1, 0, 20);
        //
        // // Moment bezwładności dla sfery 
        // sphere.I = (2 / 3) * (sphere.Mass * Math.Pow(sphere.Radius, 2));
        //
        // sphere.A = G * Math.Sin(Alpha) /
        //            (1 + (sphere.I / (sphere.Mass * Math.Pow(sphere.Radius, 2))));
        // DoSimulation(totalTime, timeStep, sphere, "sphere");
        
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }

    private static void DoSimulation(double maxTime, double timeStep, Item ball, string name)
    {
        var positionList = new List<Tuple<double, double>>();
        var angelList = new List<Tuple<double, double>>();

        Console.WriteLine($"Symulacja dla {name}:");

        for (double time = 0; time < maxTime; time += timeStep)
        {
            ball.V += ball.A * timeStep; //aktualizacja prędości liniowej

            double vx = ball.V * Math.Cos(Alpha);
            double vy = -ball.V * Math.Sin(Alpha);
            double ax = ball.A * Math.Cos(Alpha);
            double ay = -ball.A * Math.Sin(Alpha);


            ball.X = UpdatePosition(ball.X, vx, ax, timeStep);
            ball.Y = UpdatePosition(ball.Y, vy, ay, timeStep);

            ball.Beta += ball.Angle.W * timeStep;
            ball.Angle.XAngle = ball.X + ball.Radius * Math.Sin(ball.Beta);
            ball.Angle.YAngle = ball.Y + ball.Radius * Math.Cos(ball.Beta);
            ball.Angle.E = ball.A / ball.Radius ; // Poprawione obliczenie przyspieszenia kątowego
            ball.Angle.W += ball.Angle.E * timeStep;
            
            
            
            if (ball.YRounded <= 0)
            {
                break;
            }

            positionList.Add(new Tuple<double, double>(ball.XRounded, ball.YRounded));
            angelList.Add(new Tuple<double, double>(ball.Angle.XAngleRounded, ball.Angle.YAngleRounded));
        }

        Console.WriteLine("X" + "\t \t" + "Y");

        foreach (var tuple in positionList)
        {
            Console.WriteLine($"{tuple.Item1} {tuple.Item2}");
        }

        Console.WriteLine("XANGLE" + "\t \t" + "YANGLE");

        foreach (var tuple in angelList)
        {
            Console.WriteLine($"{tuple.Item1} {tuple.Item2}");
        }


        Console.WriteLine($"{name} uderzył podłogi");
    }

    static double UpdatePosition(double position, double v, double a, double timeStep)
    {
        double newPosition = position + v * timeStep + 0.5 * a * Math.Pow(timeStep, 2);
        return newPosition;
    }
}