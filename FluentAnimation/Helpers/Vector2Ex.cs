using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Helpers
{
    public static class Vector2Ex
    {
        public static Point ToPoint(this Vector2 vector)
        {
            return new Point(vector.X, vector.Y);
        }

        public static float GetMagnitude(this Vector2 vector)
        {
            return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector2 Normalize(this Vector2 vector)
        {
            return vector / vector.GetMagnitude();
        }

        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float t) => v1 * (1 - t) + (v2 * t);
    }
}
