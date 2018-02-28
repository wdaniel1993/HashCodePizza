﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodePizza.Input;
using HashCodePizza.Models;
using HashCodePizza.Output;
using HashCodePizza.Process;

namespace HashCodePizza
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dataPath = Path.Combine(basePath,@"data");

            var inputReader = new InputReader();
            var outputWriter = new OutputWriter();
            var dataProcessor = new DataProcessor();

            var input = inputReader.Read(Path.Combine(dataPath, "big.in"));

            var output = dataProcessor.Process(input);

            output.Slices = new List<Slice>()
            {
                new Slice(0,0,2,1),
                new Slice(0,2,2,2),
                new Slice(0,3,2,4)
            };

            outputWriter.Write(output, Path.Combine(dataPath, "solution.out"));
        }
    }
}
