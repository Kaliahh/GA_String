using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_String
{
    class Population
    {
        public string target;       // Den string der skal findes
        public int mutationRate; // Sandsynligheden for mutation for overkrydsning
        public int popSize;         // Befolkningsantallet

        public int generation = 0;   // Generation index
        public bool finished = false; // Indikation for om løsningen er fundet
        public Chromosome best;       // Den string der er kommet tættest på, eller når programmet er færdigt, den endelige løsning

        public Chromosome[] population; // Array med alle kromosomer i befolkningen
        public List<Chromosome> matingPool = new List<Chromosome>(); // Liste med alle de individer der må reproducere

        public Random rand = new Random();

        // Constructor
        public Population(string target, int mutationRate, int popSize)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.mutationRate = mutationRate;
            this.popSize = popSize;

            this.population = new Chromosome[popSize];

            best = new Chromosome(target.Length, rand); // Laver et tilfældigt bedste bud

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

            //TODO: Brug det her
            int bestFitness = 0;
            for (int i = 0; i < this.popSize; i++) // Finder ud af hvor meget fitness det bedste kromosom har
            {
                if (this.population[i].fitness > bestFitness)
                {
                    bestFitness = this.population[i].fitness;
                }
            }

            // Gennemgår alle kromosomerne i befolkningen, og tilføjer dem til matingPool, baseret på deres fitness-værdi
            for (int i = 0; i < this.popSize; i++)
            {
                for (int j = 0; j < this.population[i].fitness * 2; j++)
                {
                    matingPool.Add(this.population[i]);
                }
            }

            // Hvis ingen kromosomer blev tilføjet, tilføjes hele befolkningen til matingPool
            if (matingPool.Count == 0)
            {
                for (int i = 0; i < this.popSize; i++)
                {
                    matingPool.Add(this.population[i]);
                }
            }
        }

        public void Reproduce()
        {

            //TODO: Sorter befolkningen, så dele af den kan gøres helt tilfældig for at holde variationen i gang

            for (int i = 0; i < this.popSize - (this.popSize * 0.3); i++)
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

            for (int i = (int)(this.popSize - (this.popSize * 0.2)); i < this.popSize; i++)
            {
                population[i] = new Chromosome(this.target.Length, this.rand);
            }

            this.generation++; // Tæller generationstælleren op med 1
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
        }

        public void TargetCheck()
        {
            if (this.best.fitness == 100)
            {
                this.finished = true;
            }
        }
    } 
}
