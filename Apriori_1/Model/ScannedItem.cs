using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_1.Model
{
    public class ScannedItem
    {
        public string ItemSet { get; set; }

        public List<string> Items { get; set; }

        public int Count { get; set; }
    }
}
