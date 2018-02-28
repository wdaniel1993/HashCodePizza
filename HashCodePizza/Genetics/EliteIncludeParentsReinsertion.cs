using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Reinsertions;

namespace HashCodePizza.Genetics
{
    public class EliteIncludeParentsReinsertion : ReinsertionBase
    {
        private readonly IFitness _fitness;
        public EliteIncludeParentsReinsertion(IFitness fitness) : base(false, true)
        {
            _fitness = fitness;
        }

        protected override IList<IChromosome> PerformSelectChromosomes(IPopulation population, IList<IChromosome> offspring, IList<IChromosome> parents)
        {
            var newPopulation = offspring.Concat(parents).ToList();
            foreach (var chromosome in newPopulation)
            {
                if (!chromosome.Fitness.HasValue)
                {
                    chromosome.Fitness = _fitness.Evaluate(chromosome);
                }
            }
            return newPopulation.OrderByDescending(p => p.Fitness).Take(population.MaxSize).ToList();
        }
    }
}
