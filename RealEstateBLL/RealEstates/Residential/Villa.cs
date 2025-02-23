using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Villa.
    /// </summary>
    [Serializable]
    public class Villa : Residential
    {
        //Properties
        public string ListPrice { get; set; }
        public string NumGarageSpots { get; set; }
        public string GreenSpaceArea { get; set; }
    }
}
