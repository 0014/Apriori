using Apriori_1.Model;
using System.Collections.Generic;

namespace Apriori_1.Services
{
    public static class Apriori
    {
        public static List<string> Apply(List<Transaction> transactions, int support)
        {
            var list = new List<string>();

            
            return list;
        }

        public static List<Candidate> GenerateCandidates(List<Candidate> list)
        {
            var candidates = new List<Candidate>();

            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i+1; j < list.Count; j++)
                {
                    var c = CountCandidateItems(list[i].ItemSet, list[j].ItemSet);
                }
            }
            

            return candidates;
        }

        private static int CountCandidateItems(List<string> itemSet1, List<string> itemSet2)
        {
            return 0;
        }
    }
}
