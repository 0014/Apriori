using System;
using System.Collections.Generic;
using System.IO;
using Apriori_1.Extentions;
using Apriori_1.Model;

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
            var candidates = new Candidates { Level = 1 };
            //generate for candidates
            candidates = candidates.GenerateFirstLevelCandidates(transactions)
                .ScanCandidates(transactions)
                .ReturnFrequentItemList(support);
            candidates.Print();
            // generate the rest candidates untill no candidate memebers exist
            while (candidates.CandidateItems.Count != 0)
            {
                candidates = candidates.GenerateCandidates()
                    .RemoveDuplicates()
                    .ScanCandidates(transactions)
                    .ReturnFrequentItemList(support);
                    
                candidates.Print(); 
            }
            // write result in putput file
            File.WriteAllLines(output, new List<string>());
            Console.ReadKey();
        }

    }
}
