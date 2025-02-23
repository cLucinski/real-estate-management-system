using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A derived class used to represent a Bank payment.
    /// </summary>
    [Serializable]
    public class Bank : Payment
    {
        //Properties
        public string BankName { get; set; }  //txtPay1
        public string AccountNum { get; set; }  //txtPay2

        //Constructors 

        public Bank()
        {

        }

        public Bank(string bankName, string accountNum)
        {
            BankName = bankName;
            AccountNum = accountNum;
        }
    }
}
