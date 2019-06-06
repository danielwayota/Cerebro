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
            Layer[] ls1 = new Layer[] {
                new Layer(2, 3, new Tanh()),
                new Layer(3, 2, new Tanh())
            };
            Network net1 = new Network(ls1);

            Layer[] ls2 = new Layer[] {
                new Layer(2, 3, new Tanh()),
                new Layer(3, 2, new Tanh())
            };
            Network net2 = new Network(ls2);

            var result = net1.Run(new float[] { 1f, 1f });
            PrintArray(result);

            result = net2.Run(new float[] { 1f, 1f });
            PrintArray(result);

            net2.SetGenome(net1.GetGenome());

            Console.WriteLine("CLONE");

            result = net1.Run(new float[] { 1f, 1f });
            PrintArray(result);

            result = net2.Run(new float[] { 1f, 1f });
            PrintArray(result);

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
