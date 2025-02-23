using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// Abstract class for implementing Residential estates.
    /// </summary>
    [Serializable]
    public abstract class Residential : Estate
    {
        //Properties
        public ResidentialEstateType ResidentialEstateType { get; set; }
        public LegalForm LegalForm { get; set; }
        public string NumBedrooms { get; set; }
        public string NumBathrooms { get; set; }
        public string NumLevels { get; set; }
    }
}
