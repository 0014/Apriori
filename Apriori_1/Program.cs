using Apriori_1.Services;
using System.IO;

namespace Apriori_1
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] {"unclean_data.txt", "code_mapping.txt", "2", "out.txt"};

            var support = int.Parse(args[2]);
            var output = args[3];

            var transactions = CleanData.Clean(args[0], args[1]);

            var fil = Apriori.Apply(transactions, support);

            File.WriteAllLines(output, fil);

            //foreach (var transaction in transactions)
            //{
            //    Console.WriteLine($"Line : {transaction.TransactionLine}");
            //    foreach (var item in transaction.Items)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //Console.ReadKey();
        }

    }
}
