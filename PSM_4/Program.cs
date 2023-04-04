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
        double timeStep = 0.1; // Krok czasowy (s)

        // Moment bezwładności dla kuli 
        ball.I = 0.4 * (ball.Mass * Math.Pow(ball.Radius, 2));

        // Symulacja dla kuli
        Console.WriteLine("Symulacja dla kuli:");
        ball.A = G * Math.Sin(Alpha) /
                 (1 + (ball.I / (ball.Mass * Math.Pow(ball.Radius, 2)))); // przyspieszenie liniowe
        for (double time = 0; time < totalTime; time += timeStep)
        {
            ball.V += ball.A * timeStep; //aktualizacja prędości liniowej

            double Vx = ball.V * Math.Cos(Alpha);
            double Vy = -ball.V * Math.Sin(Alpha);
            double Ax = ball.A * Math.Cos(Alpha);
            double Ay = -ball.A * Math.Sin(Alpha);


            ball.X = UpdatePosition(ball.X, Vx, Ax, timeStep);
            ball.Y = UpdatePosition(ball.Y, Vy, Ay, timeStep);

            if (ball.YRounded <= 0)
            {
             Console.WriteLine("Item uderzył podłogi"); 
             return;
            }
            Console.WriteLine("X: " + ball.XRounded + " | Y: " + ball.YRounded);
            
        }
    }

    static double UpdatePosition(double position, double V, double a, double timeStep)
    {
        double newPosition = position + V * timeStep + 0.5 * a * Math.Pow(timeStep, 2);
        return newPosition;
    }
}