using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodePizza.Models
{
    public struct Slice
    {
        public Slice(int startRow, int startColumn, int endRow, int endColumn)
        {
            StartRow = startRow;
            StartColumn = startColumn;
            EndRow = endRow;
            EndColumn = endColumn;
        }

        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public int EndRow { get; set; }
        public int EndColumn { get; set; }
    }
}
