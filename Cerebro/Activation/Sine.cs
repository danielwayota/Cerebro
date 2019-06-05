using System;

namespace Cerebro.Activation
{
    public class Sine : IActivator
    {
        public float Activate(float input)
        {
            return (float)Math.Sin(input);
        }
    }
}
