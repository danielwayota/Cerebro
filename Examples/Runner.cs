using System;
using Cerebro;
using Cerebro.Activation;
using Cerebro.Genetics;

namespace Examples
{
    class Runner
    {
        static void PrintArray(float[] array) {
            foreach (var item in array)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            EvoXOR.Run();

            // Builder example
            // TODO:

            //var net2 = NNBuilder.Create()
            //    .WithInput(2)
            //    .WithLayer(Tanh, 3)
            //    .WithLayer(Sigmoid, 2)
            //    .Build();

            //var netGenes = NNBuilder.Create()
            //    .WithGenome(someGenes)
            //    .Build();

            // Genetics example
            // TODO:

            //var genome = netGenes.GetGenome();

            //var child = genome.Crossover(genome1, genome2);
            //child.Mutate();
        }
    }
}
