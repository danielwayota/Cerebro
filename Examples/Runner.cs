using System;
using Cerebro;
using Cerebro.Activation;

namespace Examples
{
    class Runner
    {
        static void Main(string[] args)
        {
            Layer[] ls = new Layer[] {
                new Layer(2, 3, new Tanh()),
                new Layer(3, 2, new Tanh())
            };

            Network net = new Network(ls);

            var result = net.Run(new float[] { 1f, 1f });

            Console.WriteLine(result[0] + " " + result[1]);

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
