using CerebroML;
using CerebroML.Activation;
using CerebroML.Factory;
using CerebroML.Genetics;

using System;
using System.Collections.Generic;

namespace Examples
{
    public class EvoXOR
    {
        public static Cerebro CreateNetwork()
        {
            Layer[] layers = new Layer[] {
                new Layer(2, 2, new Tanh()),
                new Layer(2, 1, new Sigmoid())
            };

            return new CerebroML.Cerebro(layers);
        }

        public static void Run()
        {
            // Generate first population
            int popMax = 250;
            List<CerebroML.Cerebro> population = new List<CerebroML.Cerebro>();

            BrainFactory factory = BrainFactory.Create()
                .WithWeightBiasAmplitude(10f)
                .WithInput(2)
                .WithLayer(2, LayerType.Tanh)
                .WithLayer(1, LayerType.Sigmoid);

            for (int i = 0; i < popMax; i++)
            {
                population.Add( factory.Build() );
            }

            // The record
            float kingFitness = 0;
            CerebroML.Cerebro king = null;

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
                    Console.WriteLine($"Gen {generation}");
                    Console.WriteLine($"Fitness: King- {kingFitness:0.00} AVG- {total / popMax:0.00}");

                    Console.WriteLine($" - (0, 0) = {king.Run(new float[] { 0.0f, 0.0f })[0]:0.00}");
                    Console.WriteLine($" - (1, 0) = {king.Run(new float[] { 1.0f, 0.0f })[0]:0.00}");
                    Console.WriteLine($" - (0, 1) = {king.Run(new float[] { 0.0f, 1.0f })[0]:0.00}");
                    Console.WriteLine($" - (1, 1) = {king.Run(new float[] { 1.0f, 1.0f })[0]:0.00}");
                }

                // Make new population
                List<CerebroML.Cerebro> newPop = new List<CerebroML.Cerebro>();
                for (int e = 0; e < popMax; e++)
                {
                    // Find Two parents to make cross over.
                    List<CerebroML.Cerebro> parents = new List<CerebroML.Cerebro>();

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

                    CerebroML.Cerebro offspring = factory.Build();

                    // Perform cross over and 'inject' the new Genome into the new network
                    if (parents.Count == 2)
                    {
                        Genome e1 = parents[0].GetGenome();
                        Genome e2 = parents[1].GetGenome();

                        Genome child = Genome.Crossover(e1, e2);
                        child.Mutate(0.01f, 10f);

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
                Console.WriteLine($"Fitness: King- {kingFitness:0.00}");

                Console.WriteLine($" - (0, 0) = {king.Run(new float[] { 0.0f, 0.0f })[0]:0.00}");
                Console.WriteLine($" - (1, 0) = {king.Run(new float[] { 1.0f, 0.0f })[0]:0.00}");
                Console.WriteLine($" - (0, 1) = {king.Run(new float[] { 0.0f, 1.0f })[0]:0.00}");
                Console.WriteLine($" - (1, 1) = {king.Run(new float[] { 1.0f, 1.0f })[0]:0.00}");
            }
        }

        public static float CalcFitness(CerebroML.Cerebro network)
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
