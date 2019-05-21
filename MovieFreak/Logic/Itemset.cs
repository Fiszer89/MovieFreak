using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFreak.Logic
{
    public class Itemset: List<int>
    {
        #region Properties

        public int Tid { get; set; }
        public double Support { get; set; }

        #endregion

        #region Methods

        public bool Contains(Itemset itemset)
        {
            return (this.Intersect(itemset).Count() == itemset.Count);
        }

        public Itemset Remove(Itemset itemset)
        {
            Itemset removed = new Itemset();
            removed.AddRange(from item in this
                             where !itemset.Contains(item)
                             select item);
            return (removed);
        }

        #endregion
    }
}