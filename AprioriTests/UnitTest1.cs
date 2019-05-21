using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieFreak.Logic;

namespace AprioriTests
{
    [TestClass]
    public class UnitTest1
    {
        readonly Itemset list1 = new Itemset { 1, 3, 4 };
        readonly Itemset list2 = new Itemset { 2, 3, 5 };
        readonly Itemset list3 = new Itemset { 1, 2, 3, 5 };
        readonly Itemset list4 = new Itemset { 2, 5 };
        
        //support = 50%
        private ItemsetCollection GetItems()
        {
            ItemsetCollection allItems = new ItemsetCollection();
            Itemset item1 = new Itemset {1};
            item1.Support = 50.0;
            item1.Tid = 0;
            allItems.Add(item1);
            Itemset item2 = new Itemset {3};
            item2.Support = 75.0;
            item2.Tid = 0;
            allItems.Add(item2);
            Itemset item3 = new Itemset {2};
            item3.Support = 75.0;
            item3.Tid = 0;
            allItems.Add(item3);
            Itemset item4 = new Itemset {5};
            item4.Support = 75.0;
            item4.Tid = 0;
            allItems.Add(item4);
            Itemset item5 = new Itemset { 2, 5 };
            item5.Support = 75.0;
            item5.Tid = 0;
            allItems.Add(item5);
            Itemset item6 = new Itemset { 3, 5 };
            item6.Support = 50.0;
            item6.Tid = 0;
            allItems.Add(item6);
            Itemset item7 = new Itemset { 3, 2 };
            item7.Support = 50.0;
            item7.Tid = 0;
            allItems.Add(item7);
            Itemset item8 = new Itemset { 1, 3 };
            item8.Support = 50.0;
            item8.Tid = 0;
            allItems.Add(item8);
            Itemset item9 = new Itemset { 2, 3, 5 };
            item9.Support = 50.0;
            item9.Tid = 0;
            allItems.Add(item9);

            return allItems;
        }

        //confidence = 80%
        private List<AssociationRule> GetRules()
        {
            List<AssociationRule> allRules = new List<AssociationRule>();
            AssociationRule rule1 = new AssociationRule();
            rule1.X = new Itemset { 5 };
            rule1.Y = new Itemset { 2 };
            rule1.Confidence = 100;
            allRules.Add(rule1);
            AssociationRule rule2 = new AssociationRule();
            rule2.X = new Itemset { 2 };
            rule2.Y = new Itemset { 5 };
            rule2.Confidence = 100;
            allRules.Add(rule2);
            AssociationRule rule3 = new AssociationRule();
            rule3.X = new Itemset { 1 };
            rule3.Y = new Itemset { 3 };
            rule3.Confidence = 100;
            allRules.Add(rule3);
            AssociationRule rule4 = new AssociationRule();
            rule4.X = new Itemset { 5, 3 };
            rule4.Y = new Itemset { 2 };
            rule4.Confidence = 100;
            allRules.Add(rule4);
            AssociationRule rule5 = new AssociationRule();
            rule5.X = new Itemset { 2, 3 };
            rule5.Y = new Itemset { 5 };
            rule5.Confidence = 100;
            allRules.Add(rule5);

            return allRules;
        }

        [TestMethod]
        public void Apriori() //Test of Apriori for support = 50%
        {
            ItemsetCollection collection = new ItemsetCollection { list1, list2, list3, list4 };
            ItemsetCollection L = AprioriMining.Apriori(collection, 50);
            ItemsetCollection Check = GetItems();

            Assert.AreEqual(Check.Count , L.Count); //Checking items count in itemsetcollection
            for (int k = 0; k < Check.Count; k++) //Loop for all itmesets
            {
                Assert.AreEqual(Check[k].Support, L[k].Support); //Checking support of itemset
                Assert.AreEqual(Check[k].Count, L[k].Count); // Checking number count of itmes in itemset
                Assert.AreEqual(Check[k].Sum(), L[k].Sum()); //Checking summary of all numbers in list
            }
        }

        [TestMethod]
        public void AprioriTid() //Test of AprioriTid for support = 50%
        {
            ItemsetCollection collection = new ItemsetCollection { list1, list2, list3, list4 };
            ItemsetCollection L = AprioriMining.AprioriTid(collection, 50);
            ItemsetCollection Check = GetItems();

            Assert.AreEqual(Check.Count, L.Count); //Checking items count in itemsetcollection
            for (int k = 0; k < Check.Count; k++) //Loop for all itmesets
            {
                Assert.AreEqual(Check[k].Support, L[k].Support); //Checking support of itemset
                Assert.AreEqual(Check[k].Count, L[k].Count); // Checking number count of itmes in itemset
                Assert.AreEqual(Check[k].Sum(), L[k].Sum()); //Checking summary of all numbers in list
            }
        }

        [TestMethod]
        public void Mine() //Test of minig asscociationrules for conffidence = 80
        {
            ItemsetCollection collection = new ItemsetCollection { list1, list2, list3, list4 };
            ItemsetCollection L = AprioriMining.Apriori(collection, 50);
            List<AssociationRule> AllRules = AprioriMining.Mine(collection, L, 80);
            List<AssociationRule> Check = GetRules();

            Assert.AreEqual(Check.Count, AllRules.Count); //Checking rules count in rules collection
            for (int k = 0; k < Check.Count; k++) //Loop for all rules
            {
                Assert.AreEqual(Check[k].Confidence, AllRules[k].Confidence); //Checking confidence of rule
                Assert.AreEqual(Check[k].X.Count, AllRules[k].X.Count); // Checking numbers count of itmes in X
                Assert.AreEqual(Check[k].X.Sum(), AllRules[k].X.Sum()); //Checking summary of all numbers in X
                Assert.AreEqual(Check[k].Y.Count, AllRules[k].Y.Count); // Checking numbers count of itmes in Y
                Assert.AreEqual(Check[k].Y.Sum(), AllRules[k].Y.Sum()); //Checking summary of all numbers in Y
            }
        }
    }
}
