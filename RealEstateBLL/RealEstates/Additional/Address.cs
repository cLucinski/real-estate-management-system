using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A class to contain the information of an address.
    /// </summary>
    [Serializable]
    public class Address
    {
        //Properties declared to avoid needing fields
        public string Street { get; set; }
        public string ZipCode { get; set; }  //postnummer
        public string City { get; set; }
        public Countries Country { get; set; }

        //Constructor
        public Address()
        {

        }

        //Constructor
        public Address(string street, string zipCode, string city, Countries country)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        //Override Methods
        public override string ToString()
        {
            return $"{Street}, {ZipCode} {City}, {Country}";
        }

    }
}
