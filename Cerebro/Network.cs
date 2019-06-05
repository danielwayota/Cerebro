using System;

namespace Cerebro
{
    public class Network
    {
        private Layer[] layers;

        public Network(Layer[] layers)
        {
            this.layers = layers;
        }

        public float[] Run(float[] input)
        {
            Matrix result = Matrix.From1DColum(input);

            foreach (var layer in this.layers)
            {
                result = layer.Activate(result);
            }

            return result.Flatten();
        }
    }
}
