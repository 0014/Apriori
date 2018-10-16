using System.Collections.Generic;

namespace Apriori_1.Model
{
    public class Candidate
    {
        public List<string> ItemSet { get; set; }

        public int Count { get; set; }
    }
}
