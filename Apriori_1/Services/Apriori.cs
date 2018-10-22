using System;
using Apriori_1.Model;
using System.Collections.Generic;
using System.Linq;

namespace Apriori_1.Services
{
    public static class Apriori
    {
        public static List<string> Apply(List<Transaction> transactions, int support)
        {
            var candidates = GenerateFirstLevelCandidates(transactions);
            candidates = ScanCandidates(candidates, transactions);
            candidates = ReturnFrequentItemList(candidates, transactions, support);

            var level = 1;

            Print(candidates, level);

            while (candidates.CandidateItems.Count != 0)
            {
                candidates = GenerateCandidates(candidates, transactions, level ++);
                candidates = ScanCandidates(candidates, transactions);
                candidates = ReturnFrequentItemList(candidates, transactions, support);
            }

            return candidates.CandidateItems
                .Select(_ => _.ItemSet).ToList();
        }

        private static Candidates GenerateCandidates(Candidates candidates, List<Transaction> transactions, int level)
        {
            
            return candidates;
        }

        private static Candidates ReturnFrequentItemList(Candidates candidates, List<Transaction> transactions, int support)
        {
            var list = new Candidates
            {
                Level = candidates.Level,
                CandidateItems = new List<ScannedItem>()
            };

            foreach (var items in candidates.CandidateItems)
            {
                if (items.Count >= support)
                    list.CandidateItems.Add(items);
            }

            return list;
        }

        private static Candidates ScanCandidates(Candidates candidates, List<Transaction> transactions)
        {
            foreach (var candidateItem in candidates.CandidateItems)
            {
                foreach (var transaction in transactions)
                {
                    var include = true;
                    foreach (var item in candidateItem.Items)
                    {
                        if (transaction.Items.Contains(item)) continue;

                        include = false;
                        break;
                    }
                    if (include) candidateItem.Count++;
                }
            }

            return candidates;
        }

        

        private static Candidates GenerateFirstLevelCandidates(List<Transaction> transactions)
        {
            var candidates = new Candidates
            {
                Level = 1,
                CandidateItems = new List<ScannedItem>()
            };

            foreach (var transaction in transactions)
            {
                foreach (var item in transaction.Items)
                {
                    if(!candidates.CandidateItems.Select(_ => _.ItemSet).Contains(item))
                        candidates.CandidateItems.Add(new ScannedItem
                        {
                            ItemSet = item,
                            Items = new List<string> { item },
                            Count = 0
                        });
                }
            }

            candidates.CandidateItems = candidates
                .CandidateItems
                .OrderBy(o => o.ItemSet)
                .ToList();

            return candidates;
        }

        private static void Print(Candidates list, int level)
        {
            Console.WriteLine($"Frequent Item List - Level {level}");
            foreach (var item in list.CandidateItems)
            {
                Console.WriteLine(item.ItemSet);
            }
        }
    }
}
