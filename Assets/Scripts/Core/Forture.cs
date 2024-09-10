using System;
using System.Collections.Generic;

namespace Core
{
    public abstract class Forture
    {
        private static readonly Random Random = new();

        public static int Lottery(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static double Lottery(double min, double max)
        {
            return Random.NextDouble() * (max - min) + min;
        }

        public static T LotteryEnum<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(Random.Next(values.Length));
        }

        public static T LotteryOne<T>(IList<T> elements)
        {
            if (elements.Count == 0)
            {
                throw new ArgumentException("elements is empty");
            }

            return elements[Random.Next(elements.Count)];
        }
    }
}