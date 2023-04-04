using System;
using System.Collections.Generic;
using PSM_4;

namespace RollingObjectsSimulation
{
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
        }
        
    }
}