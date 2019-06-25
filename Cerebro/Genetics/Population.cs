using System;
using CerebroML.Util;

namespace CerebroML.Genetics
{
    public abstract class Population<TEntity> where TEntity : IEntity
    {
        public int generationNumber
        {
            get; protected set;
        }

        public float mutationRate
        {
            get; set;
        }

        public float mutationAmplitude
        {
            get; set;
        }

        public TEntity[] entities
        {
            get; protected set;
        }

        private float[] fitnessList;

        public Genome best
        {
            get; protected set;
        }
        public float bestFitness
        {
            get; protected set;
        }

        /// ==============================================
        /// <summary>
        /// Sets the population and reproduction parameters
        /// </summary>
        ///
        /// <param name="entities"></param>
        /// <param name="mutationRate"></param>
        /// <param name="mutationAmplitude"></param>
        public void SetUp(TEntity[] entities, float mutationRate, float mutationAmplitude)
        {
            this.entities = entities;
            this.fitnessList = new float[entities.Length];
            this.bestFitness = 0;
            this.best = null;

            this.generationNumber = 0;
            this.mutationRate = mutationRate;
            this.mutationAmplitude = mutationAmplitude;
        }

        /// ==============================================
        /// <summary>
        /// Performs one generation
        /// </summary>
        public void Next()
        {
            this.generationNumber++;

            // Calculate the fitness for each entity
            for (int i = 0; i < this.fitnessList.Length; i++)
            {
                float f = this.GetFitness(this.entities[i], i);

                if (f > this.bestFitness)
                {
                    this.bestFitness = f;
                    this.best = this.entities[i].GetGenome();
                }

                this.fitnessList[i] = f;
            }

            Genome[] newGenomePool = new Genome[this.entities.Length];
            Genome[] parents = new Genome[2];

            // Reproduction
            for (int i = 0; i < this.entities.Length; i++)
            {
                int parentCount = 0;

                int safety = 0;
                while (parentCount < 2 && safety < 10000)
                {
                    safety++;

                    int index = (int)System.Math.Floor(StaticRandom.Next(this.entities.Length-1));

                    TEntity candidate = this.entities[index];
                    float candidateFitness = fitnessList[index];

                    float dice = StaticRandom.Next();

                    if (candidateFitness > dice)
                    {
                        parents[parentCount] = candidate.GetGenome();
                        parentCount++;
                    }
                }

                if (parents.Length == 2)
                {
                    // Crossover
                    Genome g1 = parents[0];
                    Genome g2 = parents[1];

                    Genome offspring = this.Reproduce(g1, g2);

                    newGenomePool[i] = offspring;
                }
                else
                {
                    this.OnNoParentsFound();
                }
            }

            for (int i = 0; i < this.entities.Length; i++)
            {
                this.entities[i].SetGenome(newGenomePool[i]);
                this.OnEntityReset(this.entities[i]);
            }
        }

        /// ==============================================
        /// <summary>
        /// Performs the reproduction.
        /// </summary>
        ///
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual Genome Reproduce(Genome a, Genome b)
        {
            Genome offspring = Genome.Crossover(a, b);
            offspring.Mutate(this.mutationRate, this.mutationAmplitude);

            return offspring;
        }

        /// ==============================================
        /// <summary>
        /// Gets the last generation average fitness
        /// </summary>
        ///
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public float GetAVGFitness()
        {
            float sum = 0;

            foreach (float fit in this.fitnessList)
            {
                sum += fit;
            }

            return sum / this.fitnessList.Length;
        }

        public virtual void OnNoParentsFound() {}
        public virtual void OnEntityReset(TEntity entity) {}

        public abstract float GetFitness(TEntity entity, int index);
    }
}
