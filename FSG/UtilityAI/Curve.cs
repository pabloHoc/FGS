using System;

namespace FSG.UtilityAI
{
    public enum CurveType
    {
        Linear,
        Polinomial,
        Logistic,
        Log,
        Normal,
        Sine
    }

    public class Curve
    {
        public CurveType Type { get; init; }

        public int Slope { get; init; }

        public int Exponent { get; init; }

        public int XShift { get; init; }

        public int YShift { get; init; }

        public Curve(CurveType type, int slope, int exponent, int xShift, int yShift)
        {
            Type = type;
            Slope = slope;
            Exponent = exponent;
            XShift = xShift;
            YShift = yShift;
        }

        public double ComputeValue(double x)
        {
            double value = 0.0f;

            switch (Type)
            {
                case CurveType.Linear:
                    value = Slope * (x - XShift) + YShift;
                    break;
                case CurveType.Polinomial:
                    value = Slope * Math.Pow(x - XShift, Exponent) + YShift;
                    break;
                case CurveType.Logistic:
                    value = Slope / (1 + Math.Exp(-10.0f * Exponent * (x - 0.5f - XShift))) + YShift;
                    break;
                case CurveType.Log:
                    value = (Slope * Math.Log((x - YShift) / (1.0f - (x - XShift)))) / 5.0f + 0.5f + YShift;
                    break;
                case CurveType.Normal:
                    value = Slope * Math.Exp(-30.0f * Exponent * (x - XShift - 0.5f) * (x - XShift - 0.5f)) + YShift;
                    break;
                case CurveType.Sine:
                    value = 0.5f * Slope * Math.Sin(2.0f * Math.PI * (x - XShift)) + 0.5f + YShift;
                    break;
            }

            return Math.Clamp(value, 0.0f, 1.0f);
        }
    }
}

