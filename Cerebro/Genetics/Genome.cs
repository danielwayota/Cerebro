using System;
using Cerebro.Util;

namespace Cerebro.Genetics
{
    public class Genome
    {
        public float[] Genes
        {
            get; protected set;
        }

        public int Size
        {
            get {
                return this.Genes.Length;
            }
        }

        public Genome(float[] _genes)
        {
            this.Genes = _genes;
        }

        public void Mutate(float chance)
        {
            for (int i = 0; i < this.Genes.Length; i++)
            {
                float dice = StaticRandom.Next();

                if (dice < chance)
                {
                    this.Genes[i] = StaticRandom.NextBilinear(10f);
                }
            }
        }

        public static Genome Crossover(Genome a, Genome b)
        {
            float[] offspringGenes = new float[a.Genes.Length];

            for (int i = 0; i < a.Genes.Length; i++)
            {
                offspringGenes[i] = i % 2 == 0 ? a.Genes[i] : b.Genes[i];
            }

            return new Genome(offspringGenes);
        }

        public static float[] Slice(float[] genes, int start, int end)
        {
            int count = end - start;
            float[] slice = new float[count];

            for (int i = 0; i < count; i++)
            {
                slice[i] = genes[start + i];
            }

            return slice;
        }

        public static float[] MergeGenes(float[] a, float[] b)
        {
            float[] c = new float[a.Length + b.Length];

            Array.Copy(a, c, a.Length);
            Array.Copy(b, 0, c, a.Length, b.Length);

            return c;
        }
    }
}