using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// An interface to guide the implementation of an Estate.
    /// </summary>
    public interface IEstate
    {
        //Properties
        string ID { get; set; }
        Address Address { get; set; }
        EstateType EstateType { get; set; }
        string Description { get; set; }
        string InternalSurfaceArea { get; set; }
        Image EstateImage { get; set; }
        Person Seller { get; set; }
        Person Buyer { get; set; }
        Payment Payment { get; set; }

        //Methods
        string GetFullAddress(Estate estate);
        bool IsSameEstate(Estate other);
    }
}
