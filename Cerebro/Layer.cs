using Cerebro.Activation;

namespace Cerebro
{
    public class Layer
    {
        private Matrix weights;
        private Matrix bias;

        private IActivator activator;

        public Layer(int lastNeurons, int neuronCount, IActivator activator)
        {
            this.weights = new Matrix(neuronCount, lastNeurons);
            this.weights.Randomize();

            this.bias = new Matrix(neuronCount, 1);
            this.bias.Randomize();

            this.activator = activator;
        }

        public Matrix Activate(Matrix input)
        {
            Matrix result = Matrix.Product(weights, input);
            result.Add(bias);

            result.Map(this.activator.Activate);

            return result;
        }
    }
}
