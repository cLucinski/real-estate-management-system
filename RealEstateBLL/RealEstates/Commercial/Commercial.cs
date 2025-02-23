using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// Abstract class for implementing Commercial estates.
    /// </summary>
    [Serializable]
    public abstract class Commercial : Estate
    {
        //Properties
        public CommercialEstateType CommericalEstateType { get; set; }
        public string Cost { get; set; }
        public string ZoningType { get; set; }
    }
}
