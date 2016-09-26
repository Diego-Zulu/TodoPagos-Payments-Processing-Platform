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

        public ICollection<IField> CompletedFields { get; set; } = new List<IField>();

        public Receipt(Provider aProvider, ICollection<IField> completedFields, double amountToBePaid)
        {
            CheckForNegativeAmountToBePaid(amountToBePaid);
            CheckForNullCompletedFields(completedFields);
            ReceiptProvider = aProvider;
            CompletedFields = completedFields;
            Amount = amountToBePaid;
        }

        private void CheckForNullCompletedFields(ICollection<IField> completedFields)
        {
            if (completedFields == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckForNegativeAmountToBePaid(double amountToBePaid)
        {
            if (amountToBePaid < 0)
            {
                throw new ArgumentException();
            }
        }

        public bool ContainsField(IField field)
        {
            return CompletedFields.Contains(field);
        }
    }
}
