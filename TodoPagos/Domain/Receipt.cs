using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Receipt
    {
        public Provider ReceiptProvider { get; set; }

        public Receipt(Provider aProvider)
        {
            ReceiptProvider = aProvider;
        }
    }
}
