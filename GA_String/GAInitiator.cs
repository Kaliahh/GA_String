using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GA_String
{
    public class GAInitiator
    {
        string target;    // Den string der skal findes
        int mutationRate; // Sandsynligheden for at mutation opstår ved overkrydsning
        int popSize;      // Befolkningsantal
        Population population;

        public GAInitiator(string target, int mutationRate, int popSize)
        {
            this.target = target;
            this.mutationRate = mutationRate;
            this.popSize = popSize;

            population = new Population(target, mutationRate, popSize);
        }

        public void Console_Run()
        {
            int choice = 1;

            while (choice > 0 || choice < 0) // Bliver ved indtil brugeren siger stop
            {
                Console.WriteLine("Searching for: " + "\"" + target + "\"" + " | " + target.Length);
                Console.ReadLine();

                var timer = Stopwatch.StartNew();

                

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
}
