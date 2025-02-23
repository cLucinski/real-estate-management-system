using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// An abstract class used to implement an Estate.
    /// </summary>
    [Serializable]
    public abstract class Estate : IEstate
    {
        //Implementing interface IEstate
        public string ID { get; set; } 
        public Address Address { get; set; }
        public EstateType EstateType { get; set; }
        public string Description { get; set; }
        public string InternalSurfaceArea { get; set; }
        public Image EstateImage { get; set; }
        public Person Seller { get; set; }
        public Person Buyer { get; set; }
        public Payment Payment { get; set; }

        //Constructor 
        public Estate()
        {
            Address = new Address();
            Seller = new Person();
            Buyer = new Person();
            Payment = new Payment();
        }

        //Constructor 
        public Estate(string estateID, Address estateAddress)
        {
            this.ID = estateID;
            this.Address = estateAddress;
        }

        //Methods

        /// <summary>
        /// Get string address of the estate.
        /// </summary>
        /// <param name="estate"></param>
        /// <returns>Returns a string representation of the Estate's address.</returns>
        public string GetFullAddress(Estate estate)
        {
            return $"{Address}";
        }

        /// <summary>
        /// Check against another Estate's ID to see if they are the same.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if both Estates have the same ID.</returns>
        public bool IsSameEstate(Estate other)
        {
            return this.ID == other.ID;
        }

        /// <summary>
        /// Returns a string represenation of the Estate based on its ID and Address.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string estateString = $"{ID} - {Address}";
            return estateString;
        }

    }
}
