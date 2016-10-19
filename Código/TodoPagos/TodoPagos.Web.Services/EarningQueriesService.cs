using System;
using System.Collections.Generic;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services
{
    public class EarningQueriesService : IEarningQueriesService
    {
        private readonly IUnitOfWork unitOfWork;

        public EarningQueriesService(IUnitOfWork oneUnitOfWork)
        {
            CheckForNullUnitOfWork(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null) throw new ArgumentException();
        }

        public double GetAllEarnings(DateTime from, DateTime to, string signedInUsername)
        {
            MakeSureSignedInUserHasRequiredPrivilege(signedInUsername);
            IEnumerable<Payment> allPayments = unitOfWork.PaymentRepository.Get(null, null, "");
            double earnings = 0;
            foreach (Payment payment in allPayments)
            {
                payment.AddThisPaymentsEarningsToOverallValue(ref earnings, from, to);
            }
            return earnings;
        }

        private void MakeSureSignedInUserHasRequiredPrivilege(string signedInUsername)
        {
            if (!unitOfWork.CurrentSignedInUserHasRequiredPrivilege(signedInUsername, EarningQueriesPrivilege.GetInstance()))
            {
                throw new UnauthorizedAccessException();
            }
        }

        public IDictionary<Provider, double> GetEarningsPerProvider(DateTime from, DateTime to, string signedInUsername)
        {
            MakeSureSignedInUserHasRequiredPrivilege(signedInUsername);
            IEnumerable<Payment> allPayments = unitOfWork.PaymentRepository.Get(null, null, "");
            IDictionary<Provider, double> dictionary = new Dictionary<Provider, double>();
            foreach(Payment payment in allPayments)
            {
                payment.AddThisPaymentsEarningsToDictionary(dictionary, from, to);
            }
            return dictionary;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
