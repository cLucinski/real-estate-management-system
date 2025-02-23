using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a School.
    /// </summary>
    [Serializable]
    public class School : Institutional
    {
        //Properties
        public string NumClassrooms { get; set; }
        public string NumFloors { get; set; }
    }
}
