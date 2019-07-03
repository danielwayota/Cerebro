using System;

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
            Console.WriteLine("Evolutive XOR with population wrapper");
            Console.WriteLine("---------------------------");
            // EvoXOR.Run();
            XORPopulation.Run();
            // FactoryExample.Run();
        }
    }
}
