using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain.Repository;

namespace TodoPagos.AdminForm.Logic
{
    public class ProductFacade
    {
        private IUnitOfWork unitOfWork;

        public ProductFacade(IUnitOfWork aUnitOfWork)
        {
            CheckForNullUnitOfWork(aUnitOfWork);
            unitOfWork = aUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork aUnitOfWork)
        {
            if (aUnitOfWork == null) throw new ArgumentException();
        }

    }
}
