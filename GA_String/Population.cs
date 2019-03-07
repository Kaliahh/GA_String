using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_String
{
    public class Population
    {
        public string target { get; }    // Den string der skal findes
        public int mutationRate { get; } // Sandsynligheden for mutation for overkrydsning
        public int popSize { get; }      // Befolkningsantallet

        public int generation = 0;      // Generation index
        public bool finished = false;   // Indikation for om løsningen er fundet
        public double avgFitness = 0.0; // Gennemsnits fitness for befolkningen
        public Chromosome best;         // Den string der er kommet tættest på, eller når programmet er færdigt, den endelige løsning

        public Chromosome[] population; // Array med alle kromosomer i befolkningen. Befolkningen er konstant, derfor er det et array
        public List<Chromosome> matingPool = new List<Chromosome>(); // Liste med alle de individer der må reproducere. matinPool's størrelse ændrer sig hele tiden, derfor liste

        private Random rand { get; } = new Random(); // Random object til at generere tilfældige tal

        // Constructor
        public Population(string target, int mutationRate, int popSize)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.mutationRate = mutationRate;
            this.popSize = popSize;

            this.population = new Chromosome[popSize];

            best = new Chromosome(target.Length, rand); // Laver et tilfældigt bedste bud, bare så den ikke er null

            for (int i = 0; i < this.popSize; i++)
            {
                this.population[i] = new Chromosome(target.Length, rand);
            }

        }

        // Gør alle de magiske GA ting
        public void DoMagic()
        {
            this.EvaluateFitness(); // Evaluerer fitness-værdien for hvert kromosom
            this.Select();          // Udvælger de kromosomer der skal reproducere
            this.Reproduce();       // Laver en ny generation
            this.TargetCheck();     // Checker om løsningen er fundet
        }

        // Udvælger de bedste kromosomer og gemmer dem i matingPool listen
        public void Select()
        {
            this.matingPool = new List<Chromosome>();

            double bestFitness = FindBest();

            // Gennemgår alle kromosomerne i befolkningen, og tilføjer dem til matingPool, baseret på deres fitness-værdi
            for (int i = 0; i < this.popSize; i++)
            {
                double popFitness = this.population[i].fitness;             // Konverterer fitness-værdien for et kromosom til double
                double matingWeight = popFitness.Map(0, bestFitness, 0, 1); // Mapper fitness-værdien fra 0 til 1, baseret på den bedste fitness-værdi
                int matingCount = (int)matingWeight * 100;                  // Ganger det med et forholdsvist magisk tal for at det "bliver til noget"
                AddToPool(this.population[i], matingCount);                 // Tilføjer kromosomet til matingPool matingCount gange
            }
        }

        // Finder ud af hvor meget fitness det bedste kromosom har
        public double FindBest()
        {
            double bestFitness = 0;

            for (int i = 0; i < this.popSize; i++)
            {
                if (this.population[i].fitness > bestFitness)
                {
                    bestFitness = this.population[i].fitness;
                }
            }

            return bestFitness;
        }

        // Tilføjer kromosomet til matingPool et antal gange baseret på repWeight
        public void AddToPool(Chromosome chromosome, int matingCount)
        {
            for (int i = 0; i < matingCount; i++) 
            {
                this.matingPool.Add(chromosome);
            }
        }

        public void Reproduce()
        {
            for (int i = 0; i < this.popSize; i++)
            {
                int parentIndexA = this.rand.Next(this.matingPool.Count); // Forælder A index i matingPool
                int parentIndexB = this.rand.Next(this.matingPool.Count); // Forælder B index i matingPool

                // TODO: Sørg for at begge forældre er forskellige
                Chromosome parentA = this.matingPool[parentIndexA]; // Forælder A kromosom
                Chromosome parentB = this.matingPool[parentIndexB]; // Forælder B kromosom

                Chromosome child = parentA.Crossover(parentB, this.rand); // Barnet som produkt af forældrenes gener
                child.Mutate(this.mutationRate, this.rand); // Muterer barnet
                this.population[i] = child; // Tilføjer barnet til befolkningen
            }

            this.generation++; // Der er blevet dannet en ny generation
        }

        // Gennemgår alle kromosomer i befolkningen, og beregner deres fitness-værdi
        public void EvaluateFitness()
        {
            for (int i = 0; i < this.popSize; i++)
            {
                this.population[i].CalcFitness(this.target);

                // Checker om kromosomet er bedre end det bedste kromosom
                if (this.population[i].fitness > this.best.fitness) 
                {
                    this.best = this.population[i];
                }
            }

            CalcAvgFitness();
        }

        public void CalcAvgFitness()
        {
            double totalFitness = 0;
            for (int i = 0; i < this.popSize; i++)
            {
                totalFitness += this.population[i].fitness;
            }

            this.avgFitness = totalFitness / popSize;
        }

        // Checker bare om fitness er 100
        public void TargetCheck()
        {
            if (this.best.fitness == 100)
            {
                this.finished = true;
            }
        }
    } 
}
