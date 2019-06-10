using System;
using Cerebro;
using Cerebro.Activation;
using Cerebro.Genetics;

namespace Examples
{
    class Runner
    {
        static void PrintArray(float[] array) {
            foreach (var item in array)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Evolutive XOR");
            Console.WriteLine("---------------------------");
            EvoXOR.Run();
            // FactoryExample.Run();
        }
    }
}
