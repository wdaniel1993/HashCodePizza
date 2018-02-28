using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Reinsertions;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Threading;
using HashCodePizza.Genetics;
using HashCodePizza.Models;

namespace HashCodePizza.Process
{
    public class DataProcessor
    {
        public OutputModel Process(InputModel input)
        {
            var toppingCount = Enum.GetValues(typeof(Topping)).Length;
            var sliceMasks = GeneratePossibleSliceMasks(input.MinToppings*toppingCount,input.MaxCells).ToList();
            var validSlices = new ConcurrentBag<Slice>();

            Parallel.ForEach(sliceMasks, mask =>
            {
                for (var i = 0; i < input.Rows - mask.Width; i++)
                {
                    for (var j = 0; j < input.Columns - mask.Height; j++)
                    {
                        var slice = new Slice(i,j,i+mask.Width,j+mask.Height);
                        if (IsValidSlice(input,slice))
                        {
                            validSlices.Add(slice);
                        }
                    }
                }
            });

            var orderedSlices = validSlices.ToList();

            var mutationRate = 1f;
            var crossOverRate = 1f;

            RandomizationProvider.Current = new FastRandomRandomization();

            var fitness = new PizzaCutterFitness(input,orderedSlices);
            var selection = new EliteSelection();
            var crossOver = new EvolutionStrategyCrossOver();
            var mutation = new FlipBitMutation();
            var reinsertion = new EliteIncludeParentsReinsertion(fitness);
            var currentBest = new PizzaCutterChromosome(orderedSlices.Count, (input.Rows*input.Columns) / (input.MinToppings* toppingCount));
            currentBest.Fitness = fitness.Evaluate(currentBest);
            var population = new Population(10, 20, currentBest);

            

            var ga = new GeneticAlgorithm(population, fitness, selection, crossOver, mutation)
            {
                Reinsertion = reinsertion,
                Termination = new FitnessStagnationTermination(),
                CrossoverProbability = crossOverRate,
                MutationProbability = mutationRate,
                TaskExecutor = new SmartThreadPoolTaskExecutor()
                {
                    MinThreads = Environment.ProcessorCount,
                    MaxThreads = Environment.ProcessorCount
                }
            };


            ga.GenerationRan += (sender, args) =>
            {
                if (ga.BestChromosome.Fitness > currentBest.Fitness)
                {
                    currentBest = ga.BestChromosome.Clone() as PizzaCutterChromosome;
                }
            };

            ga.Start();

            var output = new OutputModel()
            {
                Slices = currentBest.GetSlices(orderedSlices).ToList()
            };

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
