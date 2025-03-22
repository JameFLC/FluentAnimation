using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Helpers
{
    public static class FloatEx
    {
        public static float Clamped(this float value, float min, float max)
        {
            return MathF.Min(max, Math.Max(min, value));
        }
    }
}
