using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFreak.Logic
{
    public class ItemsetCollection: List<Itemset>
    {
        #region Methods

        public Itemset GetUniqueItems()
        {
            Itemset unique = new Itemset();

            foreach (Itemset itemset in this)
            {
                unique.AddRange(from item in itemset
                                where !unique.Contains(item)
                                select item);
            }

            return (unique);
        }  
        #endregion
    }
}