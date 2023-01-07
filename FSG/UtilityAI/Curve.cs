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
        private readonly CurveType _type;

        private readonly int _slope;

        private readonly int _exponent;

        private readonly int _xShift;

        private readonly int _yShift;

        public Curve(CurveType type, int slope, int exponent, int xShift, int yShift)
        {
            _type = type;
            _slope = slope;
            _exponent = exponent;
            _xShift = xShift;
            _yShift = yShift;
        }

        public double ComputeValue(double x)
        {
            double value = 0.0f;

            switch (_type)
            {
                case CurveType.Linear:
                    value = _slope * (x - _xShift) + _yShift;
                    break;
                case CurveType.Polinomial:
                    value = _slope * Math.Pow(x - _xShift, _exponent) + _yShift;
                    break;
                case CurveType.Logistic:
                    value = _slope / (1 + Math.Exp(-10.0f * _exponent * (x - 0.5f - _xShift))) + _yShift;
                    break;
                case CurveType.Log:
                    value = (_slope * Math.Log((x - _yShift) / (1.0f - (x - _xShift)))) / 5.0f + 0.5f + _yShift;
                    break;
                case CurveType.Normal:
                    value = _slope * Math.Exp(-30.0f * _exponent * (x - _xShift - 0.5f) * (x - _xShift - 0.5f)) + _yShift;
                    break;
                case CurveType.Sine:
                    value = 0.5f * _slope * Math.Sin(2.0f * Math.PI * (x - _xShift)) + 0.5f + _yShift;
                    break;
            }

            return Math.Clamp(value, 0.0f, 1.0f);
        }
    }
}

