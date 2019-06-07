using System;
using System.Collections.Generic;
using System.Text;
using Cerebro.Util;

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

        public int LinearSize
        {
            get { return this.Rows * this.Cols; }
        }

        /// =================================================
        /// <summary>
        /// Default Matrix constructor.
        /// The Matrix will have a 2D array with zeros at the begining.
        /// </summary>
        ///
        /// <param name="r">Row count</param>
        /// <param name="c">Column count</param>
        public Matrix(int r, int c)
        {
            this.values = new float[r, c];
        }

        /// =================================================
        /// <summary>
        /// Creates a Matrix based on a 2D float array
        /// </summary>
        ///
        /// <param name="values"></param>
        public Matrix(float[,] values)
        {
            this.values = values;
        }

        /// =================================================
        /// <summary>
        /// Resets all the values with random values between -amplitude and amplitude
        /// </summary>
        ///
        /// <param name="amplitude"></param>
        public void Randomize(float amplitude = 1.0f)
        {
            int r = this.Rows;
            int c = this.Cols;

            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    this.values[j, i] = StaticRandom.NextBilinear(amplitude);
                }
            }
        }

        /// =================================================
        /// <summary>
        /// Sets the values using some 2D array
        /// </summary>
        ///
        /// <param name="flatArray"></param>
        public void Set(float[] flatArray)
        {
            if (flatArray.Length != this.LinearSize)
            {
                throw new InvalidOperationException(
                    String.Format("The Matrix and the 1D array have diferent linear lengths. {0} != {1}", this.LinearSize, flatArray.Length)
                );
            }

            int r = this.Rows;
            int c = this.Cols;

            int k = 0;

            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    this.values[j, i] = flatArray[k];
                    k++;
                }
            }
        }

        /// =================================================
        /// <summary>
        /// Adds some Matrix to this value per value
        /// </summary>
        ///
        /// <param name="some"></param>
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
                    this.values[j, i] += some.Values[j,i];
                }
            }
        }

        /// =================================================
        /// <summary>
        /// Applies some function to the values
        /// </summary>
        ///
        /// <param name="runner"></param>
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

        /// =================================================
        /// <summary>
        /// Converts the Matrix into a 1D float array
        /// </summary>
        ///
        /// <returns></returns>
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

        // =================================================
        // Statics
        // =================================================

        /// =================================================
        /// <summary>
        /// Creates a Column matrix with some 1D array
        /// </summary>
        ///
        /// <param name="values"></param>
        /// <returns></returns>
        public static Matrix From1DColum(float[] values)
        {
            float[,] data = new float[values.GetLength(0), 1];

            for (int i = 0; i < values.GetLength(0); i++)
            {
                data[i, 0] = values[i];
            }

            return new Matrix(data);
        }

        /// =================================================
        /// <summary>
        /// Performs a Matrix multiplication and returns the result
        /// </summary>
        ///
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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
