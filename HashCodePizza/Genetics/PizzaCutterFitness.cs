using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Amib.Threading;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using HashCodePizza.Models;

namespace HashCodePizza.Genetics
{
    public class PizzaCutterFitness : IFitness
    {

        private readonly InputModel _inputModel;
        private readonly List<Slice> _slices;

        public PizzaCutterFitness(InputModel inputModel, List<Slice> slices)
        {
            _inputModel = inputModel;
            _slices = slices;
        }

        public double Evaluate(IChromosome chromosome)
        {
            var pizza = new int[_inputModel.Rows, _inputModel.Columns];
            var score = 0;
            var penalty = 0;
            var slices = (chromosome as PizzaCutterChromosome)?.GetSlices(_slices) ?? new List<Slice>();
            foreach (var slice in slices)
            {
                for (var i= slice.StartRow;i<= slice.EndRow; i++)
                {
                    for (var j = slice.StartColumn; j <= slice.EndColumn; j++)
                    {
                        pizza[i, j]++;
                    }
                }
                score++;
            }

            var everythingValid = true;
            foreach (var cell in pizza)
            {
                if (cell == 0)
                {
                    penalty += 2;
                }
                else if(cell > 1)
                {
                    penalty += (cell-1) * 3;
                    everythingValid = false;
                }
            }

            return everythingValid ? score*10 : score - penalty;
        }
    }
}
