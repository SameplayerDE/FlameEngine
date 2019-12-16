using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Utils
{
    public static class FlameMath
    {

        public static Vector2 Perpendicular(this Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        public static Vector2 AngleToVector(float angleRadians, float length)
        {
            return new Vector2((float)System.Math.Cos(angleRadians) * length, (float)System.Math.Sin(angleRadians) * length);
        }

        public static float Angle(this Vector2 vector)
        {
            return (float)System.Math.Atan2(vector.Y, vector.X);
        }

        public static float Angle(Vector2 from, Vector2 to)
        {
            return (float)System.Math.Atan2(to.Y - from.Y, to.X - from.X);
        }

    }
}
