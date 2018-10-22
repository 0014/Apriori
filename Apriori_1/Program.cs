using Apriori_1.Services;
using System;
using System.IO;

namespace Apriori_1
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] {"unclean_data.txt", "code_mapping.txt", "2", "out.txt"}; // remove me when test is over
            // get arguments
            var support = int.Parse(args[2]);
            var output = args[3];
            // clean transactions
            var transactions = CleanData.Clean(args[0], args[1]);
            // run apriori
            var outputLines = Apriori.Apply(transactions, support);
            // write result in putput file
            File.WriteAllLines(output, outputLines);
            Console.ReadKey();
        }

    }
}
