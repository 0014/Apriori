using System.Collections.Generic;

namespace Apriori_1.Model
{
    public class Candidates
    {
        public int Level { get; set; }

        public List<ScannedItem> CandidateItems { get; set; }
    }
}
