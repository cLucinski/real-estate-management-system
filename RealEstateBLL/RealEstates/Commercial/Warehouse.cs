using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Warehouse.
    /// </summary>
    [Serializable]
    public class Warehouse : Commercial
    {
        //Properties
        public string LotSurfaceArea { get; set; }
        public string NumLoadingBays { get; set; }
    }
}
