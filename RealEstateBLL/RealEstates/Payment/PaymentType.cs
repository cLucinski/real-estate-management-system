using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// Enumeration of Payment types.
    /// </summary>
    [Serializable]
    public enum PaymentType
    {
        Bank, 
        Western_Union,
        PayPal
    }
}
