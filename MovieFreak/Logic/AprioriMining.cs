using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFreak.Logic
{
    public class AprioriMining
    {
        public static double FindSupportCountingBase(Itemset itemset, List<ItemsetCollection> countingBase, ItemsetCollection itemsetCollection)
        {
            int mathCount = 0;
            foreach(ItemsetCollection collection in countingBase)
            {                
                int Count = (from i in collection
                                  where i.Contains(itemset)
                                  select i).Count();

                mathCount += Count;
            }

            double support = ((double)mathCount / (double)itemsetCollection.Count()) * 100;
            return support;
        }

        public static double FindSupport(Itemset itemset, ItemsetCollection itemsetCollection)
        {
            int mathCount = 0;
            mathCount = (from i in itemsetCollection
                         where i.Contains(itemset)
                         select i).Count();
            double support = ((double)mathCount / (double)itemsetCollection.Count()) * 100;
            return support;
        }

        public static ItemsetCollection AprioriTid(ItemsetCollection db, double supportThreshold)
        {
            Itemset I = db.GetUniqueItems();
            ItemsetCollection L = new ItemsetCollection(); //resultant large itemsets
            ItemsetCollection Li = new ItemsetCollection(); //large itemset in each iteration
            ItemsetCollection Ci = new ItemsetCollection(); //pruned itemset in each iteration
            List<ItemsetCollection> CountingBase = new List<ItemsetCollection>();

            //first iteration of countingbase structure
            foreach(Itemset itemset in db)
            {
                ItemsetCollection cbCollection = new ItemsetCollection();
                CountingBase.Add(cbCollection);
                foreach(int ele in itemset)
                {
                    Itemset cbItemset = new Itemset();
                    cbItemset.Add(ele);
                    cbCollection.Add(cbItemset);
                }
            }
            //first iteration (1-item itemsets)
            foreach (int item in I)
            {
                Ci.Add(new Itemset() { item });
            }
            
            //next iterations
            int k = 2;
            while (Ci.Count != 0)
            {
                List<ItemsetCollection> TempCountingBase = new List<ItemsetCollection>();
                //set Li from Ci (pruning)
                Li.Clear();
                foreach (Itemset itemset in Ci)
                {
                    itemset.Support = FindSupportCountingBase(itemset, CountingBase, db);
                    if (itemset.Support >= supportThreshold)
                    {
                        Li.Add(itemset);
                        L.Add(itemset);
                    }
                }

                //set Ci for next iteration (find supersets of Li)
                Ci.Clear();
                Ci.AddRange(Bit.FindSubsets(Li.GetUniqueItems(), k)); //get k-item subsets

                //next itteration of countingbase structure
                foreach (ItemsetCollection itemsetCollection in CountingBase)
                {
                    ItemsetCollection CountingBaseItemsetCollection = new ItemsetCollection();
                    Itemset tempItemset = new Itemset();
                    foreach(Itemset itemset in itemsetCollection)
                    {
                        foreach(int i in itemset)
                        {
                            tempItemset.Add(i);
                        }                    
                    }

                    foreach (Itemset item in Ci)
                    {
                        if(tempItemset.Contains(item))
                        {
                            CountingBaseItemsetCollection.Add(item);
                        }
                    }

                    if (CountingBaseItemsetCollection.Count() > 0)
                    {
                        TempCountingBase.Add(CountingBaseItemsetCollection);
                    }
                }
                CountingBase = TempCountingBase;             
                k += 1;
            }
            
            return (L);
        }

        public static ItemsetCollection Apriori(ItemsetCollection db, double supportThreshold)
        {
            Itemset I = db.GetUniqueItems();
            ItemsetCollection L = new ItemsetCollection(); //resultant large itemsets
            ItemsetCollection Li = new ItemsetCollection(); //large itemset in each iteration
            ItemsetCollection Ci = new ItemsetCollection(); //pruned itemset in each iteration

            //first iteration (1-item itemsets)
            foreach (int item in I)
            {
                Ci.Add(new Itemset() { item });
            }

            //next iterations
            int k = 2;
            while (Ci.Count != 0)
            {
                //set Li from Ci (pruning)
                Li.Clear();
                foreach (Itemset itemset in Ci)
                {
                    itemset.Support = FindSupport(itemset, db);
                    if (itemset.Support >= supportThreshold)
                    {
                        Li.Add(itemset);
                        L.Add(itemset);
                    }
                }

                //set Ci for next iteration (find supersets of Li)
                Ci.Clear();
                Ci.AddRange(Bit.FindSubsets(Li.GetUniqueItems(), k)); //get k-item subsets
                k += 1;
            }
            
            return (L);
        }

        public static ItemsetCollection AIS(ItemsetCollection db, double supportThreshold)
        {
            Itemset I = db.GetUniqueItems();
            ItemsetCollection L = new ItemsetCollection(); //resultant large itemsets
            ItemsetCollection Ci = new ItemsetCollection(); //pruned itemset in each iteration

            //first iteration (1-item itemsets)
            foreach (int item in I)
            {
                Ci.Add(new Itemset() { item });
            }

            //next iterations
            int k = 2;
            while (Ci.Count != 0)
            {
                foreach (Itemset itemset in Ci)
                {
                    itemset.Support = FindSupport(itemset, db);
                    if (itemset.Support >= supportThreshold)
                    {
                        L.Add(itemset);
                    }
                }

                //set Ci for next iteration (find subsets of k)
                Ci.Clear();
                Ci.AddRange(Bit.FindSubsets(I, k)); //get k-item subsets
                k += 1;
            }
            return (L);
        }

        public static ItemsetCollection SETM(ItemsetCollection db, double supportThreshold)
        {
            Itemset I = db.GetUniqueItems();
            ItemsetCollection L = new ItemsetCollection(); //resultant large itemsets
            ItemsetCollection Ci = new ItemsetCollection(); //pruned itemset in each iteration
            ItemsetCollection dbTid = new ItemsetCollection();

            int j = 1;

            //set Tids in itemsets
            foreach(var item in db)
            {
                item.Tid = j;
                dbTid.Add(item);
                j++;
            }

            //first iteration (1-item itemsets)
            foreach (int item in I)
            {
                Ci.Add(new Itemset() { item });
            }

            //next iterations
            int k = 2;
            while (Ci.Count != 0)
            {
                foreach (Itemset itemset in Ci)
                {
                    
                    itemset.Support = FindSupport(itemset, Ci);
                    if (itemset.Support >= supportThreshold)
                    {
                        L.Add(itemset);
                    }
                }

                //set Ci for next iteration (find subsets of k of sets with Tid)
                Ci.Clear();
                foreach (var set in dbTid)
                {
                    var b = Bit.FindSubsets(set, k);
                    foreach (var bit in b)
                    {
                        bit.Tid = set.Tid;
                        Ci.Add(bit);
                    }
                }
                k += 1;
            }
            return (L);
        }

        public static List<AssociationRule> Mine(ItemsetCollection db, ItemsetCollection L, double confidenceThreshold)
        {
            List<AssociationRule> allRules = new List<AssociationRule>();

            foreach (Itemset itemset in L)
            {
                ItemsetCollection subsets = Bit.FindSubsets(itemset, 0); //get all subsets
                foreach (Itemset subset in subsets)
                {
                    double confidence = (FindSupport(itemset, db) / FindSupport(subset, db)) * 100;
                    if (confidence >= confidenceThreshold)
                    {
                        AssociationRule rule = new AssociationRule();
                        rule.X.AddRange(subset);
                        rule.Y.AddRange(itemset.Remove(subset));
                        rule.Support = FindSupport(itemset, db);
                        rule.Confidence = confidence;
                        if (rule.X.Count > 0 && rule.Y.Count > 0)
                        {
                            allRules.Add(rule);
                        }
                    }
                }
            }

            return (allRules);
        }
    }
}