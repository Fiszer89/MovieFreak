using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieFreak;
using NUnit.Framework;
using MovieFreak.Logic;


namespace UnitTests.AprioriAlgorthims
{
    class AprioriTest
    {
        readonly Itemset list1 = new Itemset { 1, 3, 4 };
        readonly Itemset list2 = new Itemset { 2, 3, 5 };
        readonly Itemset list3 = new Itemset { 1, 2, 3, 5 };
        readonly Itemset list4 = new Itemset { 2, 5 };

        //support = 50%
        private ItemsetCollection GetItems()
        {
            ItemsetCollection allItems = new ItemsetCollection();
            Itemset item1 = new Itemset { 1 };
            item1.Support = 50.0;
            item1.Tid = 0;
            allItems.Add(item1);
            Itemset item2 = new Itemset { 3 };
            item2.Support = 75.0;
            item2.Tid = 0;
            allItems.Add(item2);
            Itemset item3 = new Itemset { 2 };
            item3.Support = 75.0;
            item3.Tid = 0;
            allItems.Add(item3);
            Itemset item4 = new Itemset { 5 };
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
        
        [Test]
        private void TestApriori()
        {
            ItemsetCollection collection = new ItemsetCollection { list1, list2, list3, list4 };
            ItemsetCollection L = AprioriMining.Apriori(collection, 50);
            ItemsetCollection Check = GetItems();
            Itemset i = new Itemset();
            i.Add(1);
            i.Support = 10;
            i.Tid = 0;
            Itemset j = new Itemset();
            j.Add(1);
            j.Support = 10;
            j.Tid = 0;

            Assert.AreEqual(Check[0].Sum() , j.ToList());
        }
    }
}
