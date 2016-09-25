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
            CheckForNegativeAmountToBePaid(amountToBePaid);
            ReceiptProvider = aProvider;
            CompletedFields = completedFields;
            Amount = amountToBePaid;
        }

        private void CheckForNegativeAmountToBePaid(double amountToBePaid)
        {
            if (amountToBePaid < 0) throw new ArgumentException();
        }

        public bool ContainsField(IField field)
        {
            return CompletedFields.Contains(field);
        }
    }
}
