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

        public double Amount { get; set; }

        public List<IField> CompletedFields { get; set; } = new List<IField>();

        public Receipt(Provider aProvider, List<IField> completedFields, double amountToBePaid)
        {
            ReceiptProvider = aProvider;
            CompletedFields = completedFields;
            Amount = amountToBePaid;
        }

        public bool ContainsField(IField field)
        {
            return CompletedFields.Contains(field);
        }
    }
}
