using CerebroML;
using CerebroML.Factory;
using CerebroML.Genetics;

using System;
using System.Collections.Generic;

/**
    Simple XOR training using the population wraper
 */

namespace Examples
{
    public class XORPopulation : Population<Cerebro>
    {
        public XORPopulation()
        {
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
            this.SetUp(population.ToArray(), 0.05f, 10f);
        }

        public override float GetFitness(Cerebro entity, int index)
        {
            float[][] input = new float[][]{
                new float[] { 0.0f, 0.0f },
                new float[] { 1.0f, 0.0f },
                new float[] { 0.0f, 1.0f },
                new float[] { 1.0f, 1.0f }
            };

            float[] spectedResults = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };

            // Calculate the error using mean square error
            float error = 0f;

            for (int i = 0; i < 4; i++)
            {
                float guess = entity.Run(input[i])[0];

                error += (spectedResults[i] - guess) * (spectedResults[i] - guess);
            }

            // The fitness is the inverse of the error
            return 1 - (error / 4);
        }

        // ==============================================
        // Static runner
        // ==============================================

        public static void Run()
        {
            // Generate first population
            XORPopulation pop = new XORPopulation();

            // The record
            float kingFitness = 0;
            CerebroML.Cerebro king = BrainFactory.Create()
                .WithWeightBiasAmplitude(10f)
                .WithInput(2)
                .WithLayer(2, LayerType.Tanh)
                .WithLayer(1, LayerType.Sigmoid)
                .Build();

            // Iterate until the king has a good fitness
            while (kingFitness < 0.999) // && generation < GENERATION_LIMT)
            {
                pop.Next();

                kingFitness = pop.bestFitness;
                king.SetGenome(pop.best);

                // Write to the console the current king and the average fitness
                if (pop.generationNumber % 250 == 0)
                {
                    Console.WriteLine($"Gen {pop.generationNumber}");
                    Console.WriteLine($"Fitness: King- {kingFitness:0.00} AVG- {pop.GetAVGFitness():0.00}");
                }
            }

            // Show the final results
            if (king != null)
            {
                Console.WriteLine($"FINAL -  Gen: {pop.generationNumber}");
                Console.WriteLine($"Fitness: King- {kingFitness:0.00}");

                Console.WriteLine($" - (0, 0) = {king.Run(new float[] { 0.0f, 0.0f })[0]:0.00}");
                Console.WriteLine($" - (1, 0) = {king.Run(new float[] { 1.0f, 0.0f })[0]:0.00}");
                Console.WriteLine($" - (0, 1) = {king.Run(new float[] { 0.0f, 1.0f })[0]:0.00}");
                Console.WriteLine($" - (1, 1) = {king.Run(new float[] { 1.0f, 1.0f })[0]:0.00}");
            }
        }
    }
}
