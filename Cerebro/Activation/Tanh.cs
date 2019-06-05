using System;

namespace Cerebro.Activation
{
    public class Tanh : IActivator
    {
        public float Activate(float input)
        {
            return (float)Math.Tanh(input);
        }
    }
}
