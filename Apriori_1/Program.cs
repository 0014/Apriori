using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apriori_1.Extentions;
using Apriori_1.Model;

namespace Apriori_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new[] {"unclean_data.txt", "code_mapping.txt", "2", "out.txt"}; // remove me when test is over
            args = new[] { "practice_data.txt", "8", "out.txt"}; // remove me when test is over
            List<Transaction> transactions = new List<Transaction>();
            // get arguments
            var support = int.Parse(args[args.Length - 2]);
            var output = args[args.Length - 1];
            if (args.Length == 4)
                // clean transactions
                transactions = CleanData.Unclean(args[0], args[1]);
            else if (args.Length == 3)
                transactions = CleanData.Clean(args[0]);

            // run apriori
            var candidates = new Candidates { Level = 1 };
            //generate for candidates
            var list = new List<string>();
            candidates = candidates.GenerateFirstLevelCandidates(transactions)
                .ScanCandidates(transactions)
                .ReturnFrequentItemList(support);
            candidates.Print();
            list.AddRange(candidates.CandidateItems.Select(_ => _.ItemSet).ToList());
            // generate the rest candidates untill no candidate memebers exist
            while (candidates.CandidateItems.Count != 0)
            {
                candidates = candidates.GenerateCandidates()
                    .RemoveDuplicates()
                    .ScanCandidates(transactions)
                    .ReturnFrequentItemList(support);
                    
                candidates.Print();
                list.AddRange(candidates.CandidateItems.Select(_ => _.ItemSet).ToList());
            }
            // write result in putput file
            File.WriteAllLines(output, list);
            Console.ReadKey();
        }

    }
}
