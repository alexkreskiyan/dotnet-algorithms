using System.Collections.Generic;
using System.Diagnostics;

namespace Sorting
{
    internal abstract class AbstractSorter
    {
        private int ops;

        public (double ops, double duration) Sort(List<int> source)
        {
            ops = 0;

            var stopwatch = Stopwatch.StartNew();

            DoSort(source);

            stopwatch.Stop();

            return (ops, (double)stopwatch.ElapsedTicks);
        }

        protected abstract void DoSort(List<int> source);

        protected void AddOperation() => ops++;
    }
}