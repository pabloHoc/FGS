using System;

namespace FSG.Utils
{
    public static class Math
    {
        public static double Normalize(double x, double min, double max)
        {
            return (x - min) / max - min;
        }
    }
}

