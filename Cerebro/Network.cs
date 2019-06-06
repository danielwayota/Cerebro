using System;
using System.Collections.Generic;
using Cerebro.Genetics;

namespace Cerebro
{
    public class Network
    {
        public Layer[] Layers
        {
            get; protected set;
        }

        public int GenomeSize
        {
            get {
                int sum = 0;
                foreach (var layer in this.Layers)
                {
                    sum += layer.GenomeSize;
                }
                return sum;
            }
        }

        public Network(Layer[] layers)
        {
            this.Layers = layers;
        }

        public float[] Run(float[] input)
        {
            Matrix result = Matrix.From1DColum(input);

            foreach (var layer in this.Layers)
            {
                result = layer.Activate(result);
            }

            return result.Flatten();
        }

        public void SetGenome(Genome genome)
        {
            if (this.GenomeSize != genome.Size)
            {
                throw new InvalidOperationException(
                    string.Format("The Network and the new genome are incompatible. Sizes {0} != {1}", this.GenomeSize, genome.Size)
                );
            }

            int index = 0;
            foreach (var layer in this.Layers)
            {
                float[] genomePart = new float[layer.GenomeSize];

                for (int i = 0; i < layer.GenomeSize; i++)
                {
                    genomePart[i] = genome.Genes[index + i];
                }

                layer.SetGenes(genomePart);

                index += layer.GenomeSize;
            }
        }


        public Genome GetGenome()
        {
            List<float> genes = new List<float>();

            foreach (var layer in this.Layers)
            {
                genes.AddRange(layer.GetGenes());
            }

            return new Genome(genes.ToArray());
        }
    }
}
