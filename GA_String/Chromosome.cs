using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_String
{
    public class Chromosome
    {
        public double fitness; // Fitness-værdien for kromosomet, hvor tæt det er på en løsning
        public char[] genes;   // Generne i kromosomet, som skal forsøge at blive til den rigtige string

        // Constructor
        public Chromosome(int targetLength, Random rand)
        {
            fitness = 0.0;
            genes = new char[targetLength];

            // Fylder arrayet med tilfældige bogstaver
            for (int i = 0; i < targetLength; i++) 
            {
                genes[i] = (char)rand.Next(32, 127); // Giver et tal [32 ; 127[ og konverterer til char
            }
        }

        // Gennemgår et kromosom og sammenligner det med mål-strengen. Opdaterer kromosomets fitness-værdi
        public void CalcFitness(string target)
        {
            double points = 0.0;

            for (int i = 0; i < this.genes.Length; i++)
            {
                if (this.genes[i] == target[i])
                {
                    points++;
                }
            }

            this.fitness = (points / target.Length) * 100; // Giver en procentvis fitness-værdi tilbage
        }

        // Ét-punkts overkrydsning
        public Chromosome Crossover(Chromosome parentB, Random rand) //TODO: Tilføj 2-punkts overkrydsning
        {
            Chromosome child = new Chromosome(this.genes.Length, rand);

            int crossPoint = rand.Next(this.genes.Length); // Vælger et tilfældigt punkt i kromosomet, som bruges til overkrydsning

            for (int i = 0; i < this.genes.Length; i++)
            {
                if (i > crossPoint) // Hvis indexet er mindre end crossPoint, kommer generne fra parentA
                {
                    child.genes[i] = this.genes[i];
                }

                else // Hvis indexet er større, kommer generene fra parentB
                {
                    child.genes[i] = parentB.genes[i];
                }
            }

            return child;
        }

        // Ændrer et gen i kromosomet, hvis et tilfældigt valgt tal er mindre end mutationsraten
        public void Mutate(int mutationRate, Random rand)
        {
            for (int i = 0; i < this.genes.Length; i++)
            {
                if (rand.Next(100) <= mutationRate)
                {
                    this.genes[i] = (char)rand.Next(32, 127);
                }
            }
        }
    }
}
