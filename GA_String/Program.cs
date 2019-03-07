using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GA_String
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //string target = "Her er et eller andet langt, som programmet vil proeve at finde!";   // Den string der skal findes
            //int mutationRate = 1; // Sandsynligheden for at mutation opstår ved overkrydsning
            //int popSize = 1000;   // Befolkningsantal

            //GAInitiator ini = new GAInitiator(target, mutationRate, popSize);
            //ini.Console_Run();

            Application.Run(new GAForm());
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
