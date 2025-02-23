using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// Abstract class for implementing Institutional estates
    /// </summary>
    [Serializable]
    public abstract class Institutional : Estate
    {
        //Properties
        public InstitutionalEstateType InstitutionalEstateType { get; set; }
        public string NumOfBathrooms { get; set; }
        public string ParkingCapacity { get; set; }
    }
}
