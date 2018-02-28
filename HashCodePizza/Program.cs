using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodePizza.Input;
using HashCodePizza.Output;
using HashCodePizza.Process;

namespace HashCodePizza
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputReader = new InputReader();
            var outputWriter = new OutputWriter();
            var dataProcessor = new DataProcessor();

            var input = inputReader.Read();

            var output = dataProcessor.Process(input);

            outputWriter.Write(output);
        }
    }
}
