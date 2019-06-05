using System;
using System.Collections.Generic;
using System.Text;

namespace Cerebro
{
    public class Matrix
    {

        private float[,] values;

        public float[,] Values
        {
            get { return this.values; }
        }

        public int Rows
        {
            get { return this.values.GetLength(0); }
        }

        public int Cols
        {
            get { return this.values.GetLength(1); }
        }

        public Matrix(int r, int c)
        {
            this.values = new float[r, c];
        }

        public Matrix(float[,] values)
        {
            this.values = values;
        }

        public void Randomize(float amplitude = 1.0f)
        {
            int r = this.Rows;
            int c = this.Cols;

            Random rnd = new Random();

            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    this.values[j, i] = (float)((rnd.NextDouble() * 2) - 1) * amplitude;
                }
            }
        }

        public void Add(Matrix some)
        {
            int r = this.Rows;
            int c = this.Cols;

            if (r != some.Rows || c != some.Cols)
            {
                throw new InvalidOperationException(
                    String.Format("Cannot add matrices of diferent size. {0} {1} != {2} {3}", r, c, some.Rows, some.Cols)
                );
            }

            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    this.values[j, i] = some.Values[j,i];
                }
            }
        }

        public void Map(Func<float, float> runner)
        {
            int r = this.Rows;
            int c = this.Cols;
            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    this.values[j, i] = runner(this.values[j, i]);
                }
            }
        }

        public float[] Flatten()
        {
            float[] result = new float[this.Rows * this.Cols];
            int k = 0;
            
            for (int j = 0; j < this.Rows; j++)
            {
                for (int i = 0; i < this.Cols; i++)
                {
                    result[k] = this.values[j, i];
                    k++;
                }
            }

            return result;
        }

        public void Print()
        {
            int r = this.Rows;
            int c = this.Cols;

            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    Console.Write(this.values[j, i] + " ");
                }
                Console.WriteLine();
            }
        }

        // Statics

        public static Matrix From1DColum(float[] values)
        {
            float[,] data = new float[values.GetLength(0), 1];

            return new Matrix(data);
        }

        public static Matrix Product(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
            {
                throw new InvalidOperationException(
                    String.Format("A columns should be equal to B rows. {0} {1} != {2} {3}", a.Rows, a.Cols, b.Rows, b.Cols)
                );
            }

            float[,] newValues = new float[a.Rows, b.Cols];

            for (int j = 0; j < a.Rows; j++)
            {
                for (int i = 0; i < b.Cols; i++)
                {
                    float sum = 0;
                    for (int k = 0; k < a.Cols; k++)
                    {
                        sum += a.Values[j, k] * b.Values[k, i];
                    }

                    newValues[j, i] = sum;
                }
            }


            return new Matrix(newValues);
        }
    }
}
