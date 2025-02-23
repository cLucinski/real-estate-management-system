using RealEstateDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A class to manage a list of Estates
    /// </summary>
    public class EstateManager : ListManager<Estate>
    {
        //Constructor
        public EstateManager()
        {
            
        }

        /// <summary>
        /// Check the list for an Estate matching the given ID.
        /// </summary>
        /// <param name="checkID"></param>
        /// <returns>Returns true if there is already an Estate with and ID matching checkID</returns>
        public bool CheckForEstateWithID(string checkID)
        {
            foreach (Estate estate in GetFullList())
            {
                if (estate.ID == checkID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieve an Estate from the list based on the Estate's ID.
        /// </summary>
        /// <param name="ID">ID of the Estate to be retrieved.</param>
        /// <returns>Returns the requested Estate if it exists; null if it does not.</returns>
        public Estate GetEstateByID(string ID)
        {
            foreach (Estate estate in GetFullList())
            {
                if (estate.ID.Equals(ID))
                {
                    return estate;
                }
            }
            return null;
        }
    }
}
