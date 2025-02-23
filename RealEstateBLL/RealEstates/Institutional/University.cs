using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a University.
    /// </summary>
    [Serializable]
    public class University : Institutional
    {
        //Properties
        public string NumBuildings { get; set; }
        public string NumLectureHalls { get; set; }
        public string OnCampusResidenceCapacity { get; set; }
    }
}
