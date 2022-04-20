using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNN
{
    public static class Sigmoid
    {
        public static double Output(double p)
            => p < -45.0 ? 0.0 : p > 45.0 ? 1.0 : 1.0 / (1.0 + Math.Exp(-p));

        public static double Derivative(double value)
            => value * (1 - value);
    }
}
