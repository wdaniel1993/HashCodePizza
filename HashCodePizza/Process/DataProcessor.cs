using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodePizza.Models;

namespace HashCodePizza.Process
{
    public class DataProcessor
    {
        public OutputModel Process(InputModel input)
        {
            var sliceMasks = GeneratePossibleSliceMasks(input.MinToppings*Enum.GetValues(typeof(Topping)).Length,input.MaxCells).ToList();
            var possibleSlices = new ConcurrentBag<Slice>();

            Parallel.ForEach(sliceMasks, mask =>
            {
                for (var i = 0; i < input.Rows - mask.Width; i++)
                {
                    for (var j = 0; j < input.Columns - mask.Height; j++)
                    {
                        var slice = new Slice(i,j,i+mask.Width,j+mask.Height);
                        if (IsValidSlice(input,slice))
                        {
                            possibleSlices.Add(slice);
                        }
                    }
                }
            });

            var output = new OutputModel();

            return output;
        }

        private IEnumerable<SliceMask> GeneratePossibleSliceMasks(int minSize, int maxSize)
        {
            for (int i = 1; i <= maxSize; i++)
            {
                for (int j = 1; j <= maxSize; j++)
                {
                    var cellCount = i * j;
                    if (minSize <= cellCount && cellCount <= maxSize)
                    {
                        yield return new SliceMask()
                        {
                            Width = i,
                            Height = j
                        };
                    }
                }
            }
        }

        private bool IsValidSlice(InputModel input, Slice slice)
        {
            var counts = new int[Enum.GetValues(typeof(Topping)).Length];
            for (var i = slice.StartRow; i <= slice.EndRow; i++)
            {
                for (var j = slice.StartColumn; j <= slice.EndColumn; j++)
                {
                    counts[(int) input.Pizza[i, j]]++;
                }
            }
            return counts.All(x => x >= input.MinToppings);
        }
    }
}
