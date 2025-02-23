using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Shop.
    /// </summary>
    [Serializable]
    public class Shop : Commercial
    {
        //Properties
        public bool IsDetached { get; set; }
        public string NumOfFloors { get; set; }
    }
}
