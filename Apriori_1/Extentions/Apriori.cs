using System;
using Apriori_1.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Apriori_1.Extentions
{
    public static class Apriori
    {
        public static Candidates RemoveDuplicates(this Candidates candidates)
        {
            var list = new Candidates
            {
                Level = candidates.Level,
                CandidateItems = new List<ScannedItem>()
            };

            for (var i=0; i < candidates.CandidateItems.Count - 1; i ++)
            {
                if(candidates.CandidateItems[i].ItemSet.Equals(candidates.CandidateItems[i+1].ItemSet))
                    continue;
                
                list.CandidateItems.Add(candidates.CandidateItems[i]);
            }

            return list;
        }

        public static Candidates GenerateCandidates(this Candidates candidates)
        {
            var newCandidates = new Candidates
            {
                Level = candidates.Level + 1,
                CandidateItems = new List<ScannedItem>()
            };

            for (var i = 0; i < candidates.CandidateItems.Count; i++)
            {
                for (var j = i + 1; j < candidates.CandidateItems.Count; j++)
                {
                    var itemSet = new List<string>();
                    itemSet.AddRange(candidates.CandidateItems[i].Items);
                    itemSet.AddRange(candidates.CandidateItems[j].Items);
                    for (var k = 0; k < candidates.Level; k++)
                    {
                        if (candidates.CandidateItems[i].Items.Contains(candidates.CandidateItems[j].Items[k]))
                            itemSet.Remove(candidates.CandidateItems[j].Items[k]);
                    }
                    if(itemSet.Count == candidates.Level + 1)
                        newCandidates.CandidateItems.Add(new ScannedItem
                        {
                            Count = 0,
                            ItemSet = String.Join(",", itemSet.OrderBy(o => o).ToArray()),
                            Items = itemSet.OrderBy(o => o).ToList()
                        });
                }
            }

            newCandidates.CandidateItems = newCandidates.CandidateItems
                .Distinct().OrderBy(o => o.ItemSet).ToList();
            newCandidates.CandidateItems = newCandidates.CandidateItems
                .Distinct().OrderBy(o => o.ItemSet).ToList();

            return newCandidates;
        }

        public static Candidates ReturnFrequentItemList(this Candidates candidates, int support)
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

            list.CandidateItems = list.CandidateItems.OrderBy(o => o.ItemSet).ToList();

            return list;
        }

        public static Candidates ScanCandidates(this Candidates candidates, List<Transaction> transactions)
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

        public static Candidates GenerateFirstLevelCandidates(this Candidates candidates, List<Transaction> transactions)
        {
            candidates = new Candidates
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

        public static void Print(this Candidates list)
        {
            Console.WriteLine($"------------------------------------------------------------");
            Console.WriteLine($"Frequent Item List - Level {list.Level}, {list.CandidateItems.Count} Items");
            foreach (var item in list.CandidateItems)
            {
                Console.WriteLine($"Items: {item.ItemSet}, Count: {item.Count}");
            }
        }
    }
}
