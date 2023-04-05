using System;
using System.Collections.Generic;
using PSM_4;

class Program
{
    private const double Alpha = 30 * (Math.PI / 180); // Kąt nachylenia równi w radianach (30 stopni)
    private const double G = 9.81; // Przyspieszenie ziemskie (m/s^2)

    static void Main(string[] args)
    {
        var ball = new Item(0.1, 1, 0, 20);

        double totalTime = 5.0; // Czas symulacji (s)
        double timeStep = 0.05; // Krok czasowy (s)

        // Moment bezwładności dla kuli 
        ball.I = 0.4 * (ball.Mass * Math.Pow(ball.Radius, 2));

        // Symulacja dla kuli
        ball.A = G * Math.Sin(Alpha) /
                 (1 + (ball.I / (ball.Mass * Math.Pow(ball.Radius, 2)))); // przyspieszenie liniowe
        DoSimulation(totalTime, timeStep, ball, "ball");
        Console.WriteLine();
        // Symulacja dla sfery
        var sphere = new Item(0.1, 1, 0, 20);

        // Moment bezwładności dla kuli 
        sphere.I = (2 / 3) * (sphere.Mass * Math.Pow(sphere.Radius, 2));

        sphere.A = G * Math.Sin(Alpha) /
                   (1 + (sphere.I / (sphere.Mass * Math.Pow(sphere.Radius, 2))));
        DoSimulation(totalTime, timeStep, sphere, "sphere");

        // //- - - - - - - - - - - - - doAngleRotationSimulation - - - - - - - - - - - - -
        // Console.WriteLine("ZACZYNAMY");
        // ball = new Item(0.1, 1, 0, 20);
        // ball.I = 0.4 * (ball.Mass * Math.Pow(ball.Radius, 2));
        //
        // // Symulacja dla kuli
        // ball.A = G * Math.Sin(Alpha) /
        //          (1 + (ball.I / (ball.Mass * Math.Pow(ball.Radius, 2)))); // przyspieszenie liniowe
        // for (double time = 0; time < totalTime; time += timeStep)
        // {
        //     ball.V += ball.A * timeStep; //aktualizacja prędości liniowej
        //
        //     double vx = ball.V * Math.Cos(Alpha);
        //     double vy = -ball.V * Math.Sin(Alpha);
        //     double ax = ball.A * Math.Cos(Alpha);
        //     double ay = -ball.A * Math.Sin(Alpha);
        //
        //
        //     ball.X = UpdatePosition(ball.X, vx, ax, timeStep);
        //     ball.Y = UpdatePosition(ball.Y, vy, ay, timeStep);
        //
        //
        //     UpdateAngularValues(ball, timeStep);
        //     UpdateRotation(ball, timeStep);
        //     if (ball.YRounded <= 0)
        //     {
        //         return;
        //     }
        //
        //     Console.WriteLine($"Czas: {time:0.00} s, X: {ball.XRounded}, Y: {ball.YRounded}, " +
        //                       $"Theta: {ball.Theta * 180 / Math.PI:0.00} deg");
        // }
    }

    private static void UpdateRotation(Item item, double timeStep)
    {
        item.Theta += item.W * timeStep; // θ = θ + ω * Δt
    }

    private static void UpdateAngularValues(Item item, double timeStep)
    {
        double angularAcceleration = item.Mass * G * Math.Sin(Alpha) / item.I; // ε = (m * g * sin(α)) / I
        item.W += angularAcceleration * timeStep; // ω = ω + ε * Δt
    }


    private static void DoSimulation(double totalTime, double timeStep, Item ball, string name)
    {
        var positionList = new List<Tuple<double, double>>();

        Console.WriteLine($"Symulacja dla {name}:");

        Console.WriteLine("X" + "\t \t" + "Y");
        for (double time = 0; time < totalTime; time += timeStep)
        {
            ball.V += ball.A * timeStep; //aktualizacja prędości liniowej

            double vx = ball.V * Math.Cos(Alpha);
            double vy = -ball.V * Math.Sin(Alpha);
            double ax = ball.A * Math.Cos(Alpha);
            double ay = -ball.A * Math.Sin(Alpha);


            ball.X = UpdatePosition(ball.X, vx, ax, timeStep);
            ball.Y = UpdatePosition(ball.Y, vy, ay, timeStep);


            //kat obrotu
            UpdateAngularValues(ball, timeStep);
            UpdateRotation(ball, timeStep);
            if (ball.YRounded <= 0)
            {
                break;
            }
            positionList.Add(new Tuple<double, double>(ball.XRounded,ball.YRounded));
            // Console.WriteLine(ball.XRounded + " " + ball.YRounded);
            
        }
        foreach (var tuple in positionList)
        {
            Console.WriteLine($"({tuple.Item1}, {tuple.Item2})");
        }
        Console.WriteLine($"{name} uderzył podłogi");

        
    }

    static double UpdatePosition(double position, double v, double a, double timeStep)
    {
        double newPosition = position + v * timeStep + 0.5 * a * Math.Pow(timeStep, 2);
        return newPosition;
    }
}