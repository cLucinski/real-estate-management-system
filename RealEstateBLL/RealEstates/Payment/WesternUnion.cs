using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Western Union payment.
    /// </summary>
    [Serializable]
    public class WesternUnion : Payment
    {
        //Properties
        public string AccountNum { get; set; }  //txtPay1

        //Constructors 

        public WesternUnion()
        {

        }

        public WesternUnion(string accountNum)
        {
            AccountNum = accountNum;
        }
    }
}
