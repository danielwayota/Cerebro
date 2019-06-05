using System;

namespace Cerebro.Activation
{
    public class Sigmoid : IActivator
    {
        public float Activate(float input)
        {
            return 1f / (float)(1 + Math.Exp(-input));
        }
    }
}
