//Class Info: CS634-101 Data Mining
//Author: Arif Gencosmanoglu
//File Name: cs634_arif-gencosmanoglu_apriori_8..txt
//Due: 11:59pm on Monday October 29
//Purpose: Finding the frequently bought items based on transaction data
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
            //args = new[] {"unclean_data.txt", "code_mapping.txt", "8", "cs634_arif-gencosmanoglu_apriori_8.txt" }; // remove me when test is over
            //args = new[] { "practice_data.txt", "8", "out.txt"}; // remove me when test is over
            if (args.Length != 3 || args.Length != 4)
            {
                Console.WriteLine("You must enter either 3 or 4 different inputs.");
                return;
            }

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
            // add the header lines
            var list = new List<string>
            {
                "Class Info: CS634-101 Data Mining",
                "Author: Arif Gencosmanoglu",
                $"File Name: {Path.GetFileName(output)}",
                "Due: 11:59pm on Monday October 29",
                "Purpose: Finding the frequently bought items based on transaction data"
            };
            //generate for candidates
            candidates = candidates.GenerateFirstLevelCandidates(transactions)
                .ScanCandidates(transactions)
                .ReturnFrequentItemList(support);
            candidates.Print();
            list.AddRange(candidates.CandidateItems.Select(_ => $"{_.ItemSet} ({_.Count})").ToList());
            // generate the rest candidates untill no candidate memebers exist
            while (candidates.CandidateItems.Count != 0)
            {
                candidates = candidates.GenerateCandidates()
                    .RemoveDuplicates()
                    .ScanCandidates(transactions)
                    .ReturnFrequentItemList(support);
                    
                candidates.Print();
                list.AddRange(candidates.CandidateItems.Select(_ => $"{_.ItemSet} ({_.Count})").ToList());
            }
            // write result in putput file
            File.WriteAllLines(output, list);
            Console.WriteLine("Press any key to end the application.");
            Console.ReadKey();
        }

    }
}
