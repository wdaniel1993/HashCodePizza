using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodePizza.Models
{
    public class InputModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int MinToppings { get; set; }
        public int MaxCells { get; set; }
        public Topping[][] Pizza { get; set; }
    }
}
