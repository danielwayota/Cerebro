using Cerebro;
using Cerebro.Activation;
using Cerebro.Factory;
using Cerebro.Genetics;

using System;
using System.Collections.Generic;

namespace Examples
{
    public class EvoXOR
    {
        public static Network CreateNetwork()
        {
            Layer[] layers = new Layer[] {
                new Layer(2, 2, new Tanh()),
                new Layer(2, 1, new Sigmoid())
            };

            return new Network(layers);
        }

        public static void Run()
        {
            // Generate first population
            int popMax = 250;
            List<Network> population = new List<Network>();

            Factory factory = Factory.Create()
                .WithInput(2)
                .WithLayer(2, LayerType.Tanh)
                .WithLayer(1, LayerType.Sigmoid);

            for (int i = 0; i < popMax; i++)
            {
                population.Add( factory.Build() );
            }

            // The record
            float kingFitness = 0;
            Network king = null;

            int generation = 0;

            // Iterate until the king has a good fitness
            while (kingFitness < 0.9) // && generation < GENERATION_LIMT)
            {
                generation++;
                float total = 0;

                // Search for the best one
                for (int e = 0; e < popMax; e++)
                {
                    float f = CalcFitness(population[e]);

                    if (f > kingFitness)
                    {
                        kingFitness = f;
                        king = population[e];
                    }

                    total += f;
                }

                // Write to the console the current king and the average fitness
                if (generation % 250 == 0)
                {
                    Console.WriteLine(String.Format("Gen {0}", generation));
                    Console.WriteLine(String.Format("Fitness: King- {1:0.00} AVG- {0:0.00}", total / popMax, kingFitness));

                    Console.WriteLine(String.Format(" - (0, 0) = {0:0.00}", king.Run(new float[] { 0.0f, 0.0f })[0]));
                    Console.WriteLine(String.Format(" - (1, 0) = {0:0.00}", king.Run(new float[] { 1.0f, 0.0f })[0]));
                    Console.WriteLine(String.Format(" - (0, 1) = {0:0.00}", king.Run(new float[] { 0.0f, 1.0f })[0]));
                    Console.WriteLine(String.Format(" - (1, 1) = {0:0.00}", king.Run(new float[] { 1.0f, 1.0f })[0]));
                }

                // Make new population
                List<Network> newPop = new List<Network>();
                for (int e = 0; e < popMax; e++)
                {
                    // Find Two parents to make cross over.
                    List<Network> parents = new List<Network>();

                    int control = 0;
                    Random rnd = new Random();
                    while (parents.Count < 2 && control < 10000)
                    {
                        control++;
                        int index = rnd.Next(popMax);
                        float dice = (float)rnd.NextDouble();

                        if (CalcFitness(population[index]) > dice)
                        {
                            parents.Add(population[index]);
                        }
                    }

                    Network offspring = factory.Build();

                    // Perform cross over and 'inject' the new Genome into the new network
                    if (parents.Count == 2)
                    {
                        Genome e1 = parents[0].GetGenome();
                        Genome e2 = parents[1].GetGenome();

                        Genome child = Genome.Crossover(e1, e2);
                        child.Mutate(0.01f);

                        offspring.SetGenome(child);
                    }
                    newPop.Add(offspring);
                }
                population = newPop;
            }

            // Show the final results
            if (king != null)
            {
                Console.WriteLine("FINAL");
                Console.WriteLine(String.Format("Fitness: King- {0:0.00}", kingFitness));

                Console.WriteLine(String.Format(" - (0, 0) = {0:0.00}", king.Run(new float[] { 0.0f, 0.0f })[0]));
                Console.WriteLine(String.Format(" - (1, 0) = {0:0.00}", king.Run(new float[] { 1.0f, 0.0f })[0]));
                Console.WriteLine(String.Format(" - (0, 1) = {0:0.00}", king.Run(new float[] { 0.0f, 1.0f })[0]));
                Console.WriteLine(String.Format(" - (1, 1) = {0:0.00}", king.Run(new float[] { 1.0f, 1.0f })[0]));
            }
        }

        public static float CalcFitness(Network network)
        {
            float[][] input = new float[][]{
                new float[] { 0.0f, 0.0f },
                new float[] { 1.0f, 0.0f },
                new float[] { 0.0f, 1.0f },
                new float[] { 1.0f, 1.0f }
            };

            float[] spectedResults = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };

            float fitness = 0f;

            for (int i = 0; i < 4; i++)
            {
                float guess = network.Run(input[i])[0];

                float error = spectedResults[i] - guess;

                error = (error < 0) ? error * (-1) : error;
                error = (error > 1) ? 1 : error;
                error = 1 - error;

                fitness += (error * .25f);
            }

            return fitness;
        }
    }
}
