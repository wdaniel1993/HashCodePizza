using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using HashCodePizza.Models;

namespace HashCodePizza.Genetics
{
    [Serializable]
    public sealed class PizzaCutterChromosome : BinaryChromosomeBase
    {
        public PizzaCutterChromosome(int length, bool blank = false) : base(length)
        {
            if (blank)
            {
                for (int i = 0; i < length; i++)
                {
                    ReplaceGene(i, new Gene(0));
                }
            }
            else
            {
                CreateGenes();
            }
        }

        public override IChromosome CreateNew()
        {
            return new PizzaCutterChromosome(Length,true);
        }

        public IEnumerable<Slice> GetSlices(List<Slice> slices)
        {
            var genes = GetGenes();
            return slices.Where((slice, i) => (int) genes[i].Value == 1);
        }

    }
}
