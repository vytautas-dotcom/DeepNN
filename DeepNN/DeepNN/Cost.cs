using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNN
{
    public static class Cost
    {
        public static double Error(double value, double target)
            => 0.5 * (target - value) * (target - value);

        public static double Derivative(double value, double target)
            => (-1) * (target - value);
    }
}
