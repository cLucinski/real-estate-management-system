using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// An abstract class used to implement an Apartment.
    /// </summary>
    [Serializable]
    public abstract class Apartment : Residential
    {
        //Properties
        public string FloorNum { get; set; }
        public bool ElevatorAccess { get ; set; }
    }
}
