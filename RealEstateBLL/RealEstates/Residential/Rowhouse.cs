using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Rowhouse.
    /// </summary>
    [Serializable]
    public class Rowhouse : Villa
    {
        //Properties
        public bool HasDriveway { get; set; }
        public bool HasPrivYard { get; set; }
    }
}
