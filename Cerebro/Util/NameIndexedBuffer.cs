using System.Collections.Generic;

namespace CerebroML.Util
{
    class NameIndexedBuffer
    {
        protected Dictionary<string, int> indexDict;
        protected List<float> buffer;

        public float[] data
        {
            get { return this.buffer.ToArray(); }
        }

        public int size
        {
            get { return this.buffer.Count; }
        }

        /// ============================================
        /// <summary>
        /// Default constructor
        /// </summary>
        public NameIndexedBuffer()
        {
            this.buffer = new List<float>();
            this.indexDict = new Dictionary<string, int>();
        }

        /// ============================================
        /// <summary>
        /// Adds some name indexed values
        /// </summary>
        ///
        /// <param name="name"></param>
        /// <param name="size"></param>
        public void AddIndex(string name, int size)
        {
            this.indexDict.Add(name, this.buffer.Count);

            for(int i = 0; i < size; i++)
            {
                this.buffer.Add(0);
            }
        }

        /// ============================================
        /// <summary>
        /// Sets one value into the named key place.
        /// Avoids memory allocation.
        /// </summary>
        ///
        /// <param name="name"></param>
        /// <param name="v1"></param>
        public void SetData(string name, float v1)
        {
            int index = this.indexDict[name];

            this.buffer[index] = v1;
        }

        /// ============================================
        /// <summary>
        /// Sets two values into the named key place.
        /// Avoids memory allocation.
        /// </summary>
        ///
        /// <param name="name"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void SetData(string name, float v1, float v2)
        {
            int index = this.indexDict[name];

            this.buffer[index + 0] = v1;
            this.buffer[index + 1] = v2;
        }

        /// ============================================
        /// <summary>
        /// Sets three values into the named key place.
        /// Avoids memory allocation.
        /// </summary>
        ///
        /// <param name="name"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public void SetData(string name, float v1, float v2, float v3)
        {
            int index = this.indexDict[name];

            this.buffer[index + 0] = v1;
            this.buffer[index + 1] = v2;
            this.buffer[index + 2] = v3;
        }

        /// ============================================
        /// <summary>
        /// Sets an array of values into the named key place.
        /// Does not Avoid memory allocation
        /// </summary>
        ///
        /// <param name="name"></param>
        /// <param name="vs"></param>
        public void SetData(string name, float[] vs)
        {
            int index = this.indexDict[name];

            for (int i = 0; i < vs.Length; i++)
            {
                this.buffer[index + i] = vs[i];
            }
        }
    }
}