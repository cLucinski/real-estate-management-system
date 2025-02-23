using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a PayPal payment.
    /// </summary>
    [Serializable]
    public class PayPal : Payment
    {
        //Properties
        public string Email { get; set; }  //txtPay1
        public string CardNum { get; set; }  //txtPay2

        //Constructors 

        public PayPal()
        {

        }

        public PayPal(string email, string cardNum)
        {
            Email = email;
            CardNum = cardNum;
        }
    }
}
