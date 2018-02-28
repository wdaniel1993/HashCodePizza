using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;

namespace HashCodePizza.Genetics
{
    public class EvolutionStrategyCrossOver : CrossoverBase
    {
        public EvolutionStrategyCrossOver() : base(1, 1)
        {
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            return parents.Select(x => x.Clone()).ToList();
        }
    }
}
