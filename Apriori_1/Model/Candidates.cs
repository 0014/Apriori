﻿//Class Info: CS634-101 Data Mining
//Author: Arif Gencosmanoglu
//File Name: cs634_arif-gencosmanoglu_apriori_8..txt
//Due: 11:59pm on Monday October 29
//Purpose: Finding the frequently bought items based on transaction data
using System.Collections.Generic;

namespace Apriori_1.Model
{
    public class Candidates
    {
        public int Level { get; set; }

        public List<ScannedItem> CandidateItems { get; set; }
    }
}
