using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Hospital.
    /// </summary>
    [Serializable]
    public class Hospital: Institutional
    {
        //Properties
        public string NumBeds { get; set; } 
        public string SpecialServices { get; set; }

        //Constructor
        public Hospital()
        {
            
        }
    }
}
