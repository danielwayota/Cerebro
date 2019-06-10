using CerebroML.Factory;

namespace Examples
{
    public class FactoryExample
    {
        public static void Run()
        {
            CerebroML.Cerebro net = Factory.Create()
                .WithInput(2)
                .WithLayer(3, LayerType.Tanh)
                .WithLayer(1, LayerType.Sigmoid)
                .Build();

            float[] values = net.Run(new float[] { 1, 0 });

            System.Console.WriteLine(values[0]);
        }
    }
}
