using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using HashCodePizza.Models;

namespace HashCodePizza.Genetics
{
    [Serializable]
    public sealed class PizzaCutterChromosome : BinaryChromosomeBase
    {
        private readonly int _maxSlices;
        public PizzaCutterChromosome(int length, int maxSlices) : base(length)
        {
            _maxSlices = maxSlices;
            for (int i = 0; i < length; i++)
            {
                ReplaceGene(i, new Gene(0));
            }

            var indices = RandomizationProvider.Current.GetInts(maxSlices, 0, length);
            foreach (var index in indices)
            {
                ReplaceGene(index, new Gene(1));
            }
        }

        public override IChromosome CreateNew()
        {
            return new PizzaCutterChromosome(Length,_maxSlices);
        }

        public IEnumerable<Slice> GetSlices(List<Slice> slices)
        {
            var genes = GetGenes();
            return slices.Where((slice, i) => (int) genes[i].Value == 1);
        }

    }
}
