using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodePizza.Models;
using SuccincT.Parsers;

namespace HashCodePizza.Input
{
    public class InputReader
    {
        public InputModel Read(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var inputParams = lines.First().Split().Select(x => x.TryParseInt().ValueOrDefault).ToList();
            var model = new InputModel()
            {
                Rows = inputParams[0],
                Columns = inputParams[1],
                MinToppings = inputParams[2],
                MaxCells = inputParams[3],
                Pizza = new Topping[inputParams[0],inputParams[1]]
            };

            var pizzaLines = lines.Skip(1).ToList();
            for (var i = 0; i<model.Rows; i++)
            {
                for (var j = 0; j < model.Columns; j++)
                {
                    model.Pizza[i,j] = pizzaLines[i][j] == 'T' ? Topping.Tomato : Topping.Mushroom;
                }
            }

            return model;
        }
    }
}
