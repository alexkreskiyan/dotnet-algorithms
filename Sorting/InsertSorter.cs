using System.Collections.Generic;

namespace Sorting
{
    class InsertSorter : AbstractSorter
    {
        protected override void DoSort(List<int> source)
        {
            int key, i, j;
            for (j = 2; j < source.Count; j++)
            {
                key = source[j];
                AddOperation();

                i = j - 1;

                while (i > 0 && source[i] > key)
                {
                    AddOperation();

                    source[i + 1] = source[i];
                    AddOperation();

                    i--;
                }

                source[i + 1] = key;
                AddOperation();
            }
        }
    }
}