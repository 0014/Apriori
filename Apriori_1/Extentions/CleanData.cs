﻿//Class Info: CS634-101 Data Mining
//Author: Arif Gencosmanoglu
//File Name: cs634_arif-gencosmanoglu_apriori_8..txt
//Due: 11:59pm on Monday October 29
//Purpose: Finding the frequently bought items based on transaction data
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apriori_1.Model;

namespace Apriori_1.Extentions
{
    public static class CleanData
    {
        public static List<Transaction> Unclean(string uncleanTxt, string mapTxt)
        {
            var uncleanTransactions= ItemizeTransaction(new List<string>(File.ReadAllLines(uncleanTxt))); 
            var map = MapBuilder(new List<string>(File.ReadAllLines(mapTxt)));

            return CleanTransactions(uncleanTransactions, map);
        }

        public static List<Transaction> Clean(string transactionFile)
        {
            var transactions = new List<string>(File.ReadAllLines(transactionFile));

            return ItemizeTransaction(transactions);
        }

        private static List<Transaction> ItemizeTransaction(List<string> rawData)
        {
            var transactions = new List<Transaction>();

            foreach (var line in rawData)
            {
                var items = line.Split(';');
                if (items.Length < 2)
                    items = line.Split(' ');

                transactions.Add(new Transaction
                {
                    TransactionLine = line,
                    Items = items.Where(x => !x.Equals("")).ToList()
                });
            }

            return transactions;
        }

        private static List<Mapping> MapBuilder(List<string> rawData)
        {
            var map = new List<Mapping>();

            foreach (var line in rawData)
            {
                var sections = line.Split(' ');

                map.Add(new Mapping
                {
                    Alias = sections[1],
                    Item = sections[0]
                });
            }

            return map;
        }

        private static List<Transaction> CleanTransactions(List<Transaction> uncleanData, List<Mapping> map)
        {
            var transactions = new List<Transaction>();

            foreach (var transaction in uncleanData)
            {
                var isValid = true;
                var items = new List<string>();

                foreach (var item in transaction.Items)
                {
                    var mapItem = map.FirstOrDefault(x => x.Alias.Equals(item) || x.Item.Equals(item));
                    if (mapItem == null)
                    {
                        isValid = false;
                        items = transaction.Items;
                        break;
                    }

                    items.Add(mapItem.Item);
                }

                if (!isValid) continue;

                transactions.Add(new Transaction
                {
                    TransactionLine = transaction.TransactionLine,
                    Items = items
                });
            }

            return transactions;
        }
    }

}

