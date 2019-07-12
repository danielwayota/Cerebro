using System;
using CerebroML.Util;

namespace Examples
{
    public class NameIndexedBufferExample
    {
        public static void Run()
        {
            var buffer = new NameIndexedBuffer();

            buffer.AddIndex("one", 1);
            buffer.AddIndex("two", 2);
            buffer.AddIndex("three", 3);
            buffer.AddIndex("any", 8);

            buffer.SetData("one", 1);
            buffer.SetData("two", 22, 44);
            buffer.SetData("three", 333, 444, 555);
            buffer.SetData("any", new float[] {81, 82, 83, 84, 85, 86, 87, 89});

            foreach (float n in buffer.data)
            {
                Console.Write($" <{n}> ");
            }
            Console.WriteLine();
        }
    }
}
