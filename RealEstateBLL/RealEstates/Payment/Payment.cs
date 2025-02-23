using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// Base class for implementing a Payment
    /// </summary>
    [Serializable]
    public class Payment
    {
        //Properties
        public int PaymentType { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }

        //Constructor
        public Payment()
        {
            PaymentType = -1;
        }

        //Constructor
        public Payment(int paymentType, string amount, string currency)
        {
            PaymentType = paymentType;
            Amount = amount;
            Currency = currency;
        }
    }
}
