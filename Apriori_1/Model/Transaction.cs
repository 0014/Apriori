using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_1.Model
{
    public class Transaction
    {
        public string TransactionLine { get; set; }

        public List<string> Items { get; set; }
    }
}
