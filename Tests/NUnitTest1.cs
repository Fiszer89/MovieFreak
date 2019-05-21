using System;
//using NUnit.Framework;
using MovieFreak.Logic;

namespace Tests
{
    //[TestFixture]
    public class NUnitTest1
    {
        readonly Itemset list1 = new Itemset { 1, 3, 4 };
        readonly Itemset list2 = new Itemset { 2, 3, 5 };
        readonly Itemset list3 = new Itemset { 1, 2, 3, 5 };
        readonly Itemset list4 = new Itemset { 2, 5 };

        //support = 50%
        private ItemsetCollection GetItems()
        {
            ItemsetCollection allItems = new ItemsetCollection();
            Itemset item1 = new Itemset();
            item1.Add(1);
            item1.Support = 1 / 2;
            Itemset item2 = new Itemset();
            item2.Add(2);
            item2.Support = 3 / 4;
            Itemset item3 = new Itemset();
            item3.Add(3);
            item3.Support = 3 / 4;
            Itemset item4 = new Itemset();
            item4.Add(5);
            item4.Support = 3 / 4;
            Itemset item5 = new Itemset { 1, 3 };
            item5.Support = 1 / 2;
            Itemset item6 = new Itemset { 2, 3 };
            item6.Support = 1 / 2;
            Itemset item7 = new Itemset { 2, 5 };
            item7.Support = 3 / 4;
            Itemset item8 = new Itemset { 2, 3, 5 };
            item8.Support = 1 / 2;
            return allItems;
        }
        //[Test]
        public void TestMethod1()
        {
            ItemsetCollection collection = new ItemsetCollection { list1, list2, list3, list4 };
            ItemsetCollection L = AprioriMining.Apriori(collection, 50);
            ItemsetCollection Check = GetItems();

            Assert.AreEqual(Check, L);
        }
    }
}