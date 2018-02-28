using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodePizza.Models;

namespace HashCodePizza.Output
{
    public class OutputWriter
    {
        public void Write(OutputModel model, string path)
        {
            var lines = new List<string>
            {
                $"{model.Slices.Count}"
            };
            lines.AddRange(model.Slices.Select(slice => $"{slice.StartRow} {slice.StartColumn} {slice.EndRow} {slice.EndColumn}"));
            File.WriteAllLines(path,lines);
        }
    }
}
