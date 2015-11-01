using System;
using System.Collections.Generic;

namespace Sorting
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var sizes = GetSizes(args);
            Sort<InsertSorter>(sizes);
        }

        private static void Sort<T>(IEnumerable<double> sizes)
            where T : AbstractSorter, new()
        {
            var prevSize = 1000.0;
            var (prevOps, prevDuration) = Sort<T>((int)prevSize);

            foreach (var size in sizes)
            {
                var (ops, duration) = Sort<T>((int)size);

                var opsIncrease = Math.Round((ops / prevOps) * (prevSize / size), 2);
                var durationIncrease = Math.Round((duration / prevDuration) * (prevSize / size), 2);
                Console.WriteLine($"Ops count increased {opsIncrease} times, Duration increased {durationIncrease} times");

                prevSize = size;
                prevOps = ops;
                prevDuration = duration;
            }
        }

        private static (double ops, double elapsed) Sort<T>(int sourceSize)
            where T : AbstractSorter, new()
        {
            var algorithm = typeof(T).Name;
            var (ops, duration) = new T().Sort(GetSource(sourceSize));
            var msDuration = Math.Round(duration / TimeSpan.TicksPerMillisecond, 3);

            Console.WriteLine($"Sorting with {algorithm} {sourceSize} items took {ops} operations and {msDuration}ms");
            Console.WriteLine($"It's {ops / sourceSize} ops per item, {duration / sourceSize} ticks per item");

            return (ops, duration);
        }

        private static IEnumerable<double> GetSizes(string[] args)
        {
            if (args.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(args), "No base size, step and steps given");

            var baseSize = double.Parse(args[0]);
            var step = double.Parse(args[1]);
            var steps = double.Parse(args[2]);

            for (var i = 0; i < steps; i++)
                yield return Math.Round(baseSize * Math.Pow(step, i));
        }

        private static List<int> GetSource(int sourceSize)
        {
            var i = 0;
            var random = new Random();
            var list = new List<int>();

            while (i++ < sourceSize)
                list.Add(random.Next(sourceSize));

            return list;
        }
    }
}