using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Receipt
    {
        public virtual Provider ReceiptProvider { get; set; }

        public double Amount { get; set; }

        public virtual ICollection<IField> CompletedFields { get; set; }

        public int ID { get; set; }

        protected Receipt()
        {
            CompletedFields = new List<IField>();
        }

        public Receipt(Provider aProvider, ICollection<IField> completedFields, double amountToBePaid)
        {
            CheckForPossibleErrors(aProvider, completedFields, amountToBePaid);
            ReceiptProvider = aProvider;
            CompletedFields = completedFields;
            Amount = amountToBePaid;
        }

        private void CheckForPossibleErrors(Provider aProvider, ICollection<IField> completedFields, double amountToBePaid)
        {
            CheckForNegativeAmountToBePaid(amountToBePaid);
            CheckForCompletedFieldsPossibleErrors(completedFields);
            CheckForNullProvider(aProvider);
            CheckForIncompleteProvider(aProvider);
            CheckThatCompletedFieldsAreProvidersFields(aProvider, completedFields);
        }

        private void CheckForCompletedFieldsPossibleErrors(ICollection<IField> completedFields)
        {
            CheckForNullCompletedFields(completedFields);
            CheckForEmptyCompletedField(completedFields);
            CheckForInvalidCompletedFields(completedFields);
        }

        private void CheckForNullProvider(Provider aProvider)
        {
            if (aProvider == null)
            {
                throw new ArgumentException("El proveedor no puede ser nulo");
            }
        }

        private void CheckForIncompleteProvider(Provider aProvider)
        {
            if (!aProvider.IsCompleteAndActive())
            {
                throw new ArgumentException("El proveedor no puede estar incompleto");
            }
        }

        private void CheckThatCompletedFieldsAreProvidersFields(Provider aProvider, ICollection<IField> completedFields)
        {
            if (!aProvider.AllTargetFieldsAndThisFieldsAreEqualNotRegardingData(completedFields))
            {
                throw new ArgumentException("Los campos completos deben ser los campos del proveedor");
            }
        }

        private void CheckForEmptyCompletedField(ICollection<IField> completedFields)
        {
            foreach (IField oneField in completedFields)
            {
                if (oneField.IsEmpty())
                {
                    throw new ArgumentException("Los campos completos no pueden estar vacíos");
                }
            }
        }

        private void CheckForNullCompletedFields(ICollection<IField> completedFields)
        {
            if (completedFields == null)
            {
                throw new ArgumentException("La lista de campos completos no puede ser nula");
            }
        }

        private void CheckForNegativeAmountToBePaid(double amountToBePaid)
        {
            if (amountToBePaid < 0)
            {
                throw new ArgumentException("El monto a pagar no puede ser negativo");
            }
        }

        public bool ContainsField(IField field)
        {
            return CompletedFields.Contains(field);
        }

        public double CalculateEarnings()
        {
            return (ReceiptProvider.Commission / 100) * Amount;
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (IsNull(obj)) return false;
                Receipt otherReceipt = (Receipt)obj;
                return SeeIfReceiptsAreEqual(otherReceipt);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private bool SeeIfReceiptsAreEqual(Receipt otherReceipt)
        {
            return ReceiptProvider.Equals(otherReceipt.ReceiptProvider)
                && CompletedFieldsListsAreEqual(CompletedFields, otherReceipt.CompletedFields)
                && Amount.Equals(otherReceipt.Amount);
        }

        private bool IsNull(object objectToCheck)
        {
            return objectToCheck == null;
        }

        private bool CompletedFieldsListsAreEqual(IEnumerable<IField> firstList, IEnumerable<IField> secondList)
        {
            return firstList.All(x => secondList.Contains(x)) && secondList.All(x => firstList.Contains(x));
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        private void CheckForInvalidCompletedFields(ICollection<IField> someCompletedFields)
        {
            foreach (IField field in someCompletedFields)
            {
                if (field.IsEmpty() || !field.IsValid())
                {
                    throw new ArgumentException("Campo inválido");
                }
            }
        }

        public int GetReceiptProviderID()
        {
            return this.ReceiptProvider.ID;
        }

        public bool IsComplete()
        {
            try
            {
                CheckForPossibleErrors(this.ReceiptProvider, this.CompletedFields, this.Amount);
                return true;
            } catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
