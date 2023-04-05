using System;
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
        DoPositionSimulation(totalTime, timeStep, ball, "ball");
        Console.WriteLine();
        // Symulacja dla sfery
        var sphere = new Item(0.1, 1, 0, 20);

        // Moment bezwładności dla kuli 
        sphere.I = (2 / 3) * (sphere.Mass * Math.Pow(sphere.Radius, 2));

        sphere.A = G * Math.Sin(Alpha) /
                   (1 + (sphere.I / (sphere.Mass * Math.Pow(sphere.Radius, 2))));
        DoPositionSimulation(totalTime, timeStep, sphere, "sphere");
    }
    
    

    private static void DoPositionSimulation(double totalTime, double timeStep, Item ball, string name)
    {
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

            if (ball.YRounded <= 0)
            {
                Console.WriteLine($"{name} uderzył podłogi");
                return;
            }

            Console.WriteLine(ball.XRounded + " " + ball.YRounded);
        }
    }

    static double UpdatePosition(double position, double v, double a, double timeStep)
    {
        double newPosition = position + v * timeStep + 0.5 * a * Math.Pow(timeStep, 2);
        return newPosition;
    }
}