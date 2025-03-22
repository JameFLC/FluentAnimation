using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Helpers
{
    public static class DoubleEx
    {
        public static double Clamped(this double value, double min, double max)
        {
            return Math.Min(max, Math.Max(min, value));
        }

    }
}
