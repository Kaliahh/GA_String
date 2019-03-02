using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string target = "Ane Soegaard Joergensen";   // Den string der skal findes
            int mutationRate = 5; // Sandsynligheden for at mutation opstår ved overkrydsning
            int popSize = 1000;     // Befolkningsantal

            Population population = new Population(target, mutationRate, popSize);

            Console.WriteLine("Søger efter: " + "\"" + target + "\"" + " | " + target.Length);
            Console.ReadLine();

            while (population.finished == false)
            {
                Console.WriteLine(population.generation + " | " + new string(population.best.genes) + ", " + population.best.fitness + " | " + population.matingPool.Count); // Printer den bedste løsning indtil videre
                population.DoMagic(); // Gør alle de der GA ting
            }

            Console.WriteLine("\n" + population.generation + " | " + new string(population.best.genes) + ", " + population.best.fitness);
            Console.WriteLine("\n\nThe End!");
            Console.ReadLine();
        }
    }
}
