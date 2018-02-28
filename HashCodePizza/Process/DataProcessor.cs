using System;
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
    }
}
