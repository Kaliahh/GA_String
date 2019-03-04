using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string target = "aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa aaaa";   // Den string der skal findes
            int mutationRate = 1; // Sandsynligheden for at mutation opstår ved overkrydsning
            int popSize = 1000;   // Befolkningsantal
            int choice = 1;

            while (choice > 0 || choice < 0) // Bliver ved indtil brugeren siger stop
            {
                Console.WriteLine("Searching for: " + "\"" + target + "\"" + " | " + target.Length);
                Console.ReadLine();

                var timer = Stopwatch.StartNew();

                Population population = new Population(target, mutationRate, popSize);

                while (population.finished == false) // Bliver ved indtil fitness-værdien når 100
                {
                    population.DoMagic(); // Gør alle de der GA ting

                    if (population.generation % 1 == 0) // Så hver generation ikke bliver printet ud
                    {
                        Console.WriteLine(population.generation + " | " + new string(population.best.genes) + ", " + Math.Round(population.best.fitness, 2) + " | " + population.matingPool.Count); // Printer den bedste løsning indtil videre
                    }
                }

                timer.Stop();

                Console.WriteLine("\n" + population.generation + " | " + new string(population.best.genes) + ", " + population.best.fitness);
                Console.WriteLine("Time elapsed: " + timer.ElapsedMilliseconds + " milliseconds");
                Console.WriteLine("\n\nThe End!");

                Console.WriteLine("\nAgain?\n0: No\n1: Yes");
                choice = int.Parse(Console.ReadLine());
            }
        }
    }

    public static class ExtensionMethods // Map funktion, til at mappe værdier fra et sæt til et andet
    {
        public static double Map(this double value, double fromInput, double toInput, double fromOutput, double toOutput)
        {
            return (value - fromInput) / (toInput - fromInput) * (toOutput - fromOutput) + fromOutput;
        }
    }
}
