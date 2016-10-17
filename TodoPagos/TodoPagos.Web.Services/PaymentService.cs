using System;
using System.Collections.Generic;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;
using System.Linq;

namespace TodoPagos.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IUnitOfWork oneUnitOfWork)
        {
            CheckForNullUnitOfWork(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null) throw new ArgumentException();
        }

        public int CreatePayment(Payment newPayment)
        {
            CheckForValidCreationOfPayment(newPayment);
            unitOfWork.PaymentRepository.Insert(newPayment);
            unitOfWork.Save();
            return newPayment.ID;
        }

        private void CheckForValidCreationOfPayment(Payment newPayment)
        {
            CheckForNullPayment(newPayment);
            LoadAllReceiptsProvidersInfoFromDatabase(newPayment);
            CheckForIncompletePayment(newPayment);
            CheckThatEqualReceiptDoesntAlreadyExistInReceiptRepository(newPayment.Receipts);
        }
        private void LoadAllReceiptsProvidersInfoFromDatabase(Payment newPayment)
        {
            ICollection<Receipt> allReceiptsInPayment = newPayment.Receipts;

            foreach (Receipt oneReceipt in allReceiptsInPayment)
            {
                int providerId = oneReceipt.GetReceiptProviderID();
                Provider receiptProviderFromDatabase = unitOfWork.ProviderRepository.GetByID(providerId);

                oneReceipt.ReceiptProvider = receiptProviderFromDatabase;
            }
        }

        private void CheckForNullPayment(Payment payment)
        {
            if (payment == null) throw new ArgumentException();
        }

        private void CheckForIncompletePayment(Payment newPayment)
        {
            if (!newPayment.IsComplete()) throw new ArgumentException();
        }

        private void CheckThatEqualReceiptDoesntAlreadyExistInReceiptRepository(IEnumerable<Receipt> receipts)
        {
            IEnumerable<Receipt> allReceipts = unitOfWork.ReceiptRepository.Get(null, null, "");
            bool anyIsContained = receipts.Any(x => allReceipts.Contains(x));
            if (anyIsContained)
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return unitOfWork.PaymentRepository.Get(null, null, "");
        }

        public Payment GetSinglePayment(int paymentId)
        {
            Payment payment = unitOfWork.PaymentRepository.GetByID(paymentId);
            CheckIfPaymentExists(payment);
            return payment;
        }

        private void CheckIfPaymentExists(Payment payment)
        {
            if (payment == null) throw new ArgumentException();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
