using System;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Rental Apartment.
    /// </summary>
    [Serializable]
    public class Rental : Apartment
    {
        //Properties
        public string MonthlyRent { get; set; }
        public bool HydroIncluded { get; set; }

        //Constructor 
        public Rental()
        {
            Address = new Address();
        }

        //Override method
        public override string ToString()
        {
            string rentalString = $"Rental - Address: {Address} ; EstateType: {ResidentialEstateType}; Legal Form: {LegalForm}; Number of Bathrooms: {NumBathrooms}";
            return rentalString;
        }
    }
}
