using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Tenement.
    /// </summary>
    [Serializable]
    public class Tenement : Apartment
    {
        //Properties
        public string ListPrice { get; set; }
    }
}
