using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFreak.Logic
{
    public class AssociationRule
    {
        #region Properties

        public Itemset X { get; set; }
        public Itemset Y { get; set; }
        public double Support { get; set; }
        public double Confidence { get; set; }

        #endregion

        #region Constructors

        public AssociationRule()
        {
            X = new Itemset();
            Y = new Itemset();
            Support = 0.0;
            Confidence = 0.0;
        }

        #endregion

        #region Methods

        public List<int> ReturnX()
        {
            return (X);
        }

        public List<int> ReturnY()
        {
            return (Y);
        }

        #endregion
    }
}