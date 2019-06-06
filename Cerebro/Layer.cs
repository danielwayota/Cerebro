using System;
using Cerebro.Activation;
using Cerebro.Genetics;

namespace Cerebro
{
    public class Layer
    {
        public int GenomeSize
        {
            get {
                return this.weights.LinearSize + this.bias.LinearSize;
            }
        }

        private Matrix weights;
        private Matrix bias;

        private IActivator activator;

        public Layer(int lastNeurons, int neuronCount, IActivator activator)
        {
            this.weights = new Matrix(neuronCount, lastNeurons);
            this.weights.Randomize(10f);

            this.bias = new Matrix(neuronCount, 1);
            this.bias.Randomize(10f);

            this.activator = activator;
        }

        public Matrix Activate(Matrix input)
        {
            Matrix result = Matrix.Product(weights, input);
            result.Add(bias);

            result.Map(this.activator.Activate);

            return result;
        }

        public void SetGenes(float[] genes)
        {
            if (this.GenomeSize != genes.Length)
            {
                throw new InvalidOperationException(
                    string.Format("The Layer and the new genome are incompatible. Sizes {0} != {1}", this.GenomeSize, genes.Length)
                );
            }
            float[] w = Genome.Slice(genes, 0, this.weights.LinearSize);
            float[] b = Genome.Slice(genes, this.weights.LinearSize, this.weights.LinearSize + this.bias.LinearSize);

            this.weights.Set(w);
            this.bias.Set(b);
        }

        public float[] GetGenes()
        {
            float[] flatweights = this.weights.Flatten();
            float[] flatbias = this.bias.Flatten();

            float[] fullValues = Genome.MergeGenes(flatweights, flatbias);

            return fullValues;
        }
    }
}
