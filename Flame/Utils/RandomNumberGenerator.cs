using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Utils
{
    public static class RandomNumberGenerator
    {

        public static int GetRandomBetween(int Min, int Max)
        {
            return Flame.Random.Next(Min, Max + 1);
        }

        public static double GetRandom()
        {
            return Flame.Random.NextDouble();
        }

    }
}
