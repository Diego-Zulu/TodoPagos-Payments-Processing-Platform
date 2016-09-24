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

        public List<IField> CompletedFields { get; set; } = new List<IField>();

        public Receipt(Provider aProvider, List<IField> completedFields)
        {
            ReceiptProvider = aProvider;
            CompletedFields = completedFields;
        }

        public bool ContainsField(IField field)
        {
            return CompletedFields.Contains(field);
        }
    }
}
