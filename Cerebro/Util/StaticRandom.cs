namespace Cerebro.Util
{
    public class StaticRandom
    {
        private static System.Random sysrnd;

        static StaticRandom() {
            sysrnd = new System.Random();
        }

        public static float Next(float amplitude = 1f)
        {
            return (float)(sysrnd.NextDouble() * amplitude);
        }

        public static float NextBilinear(float amplitude = 1f)
        {
            return (float) ((sysrnd.NextDouble() * 2) - 1) * amplitude;
        }
    }
}