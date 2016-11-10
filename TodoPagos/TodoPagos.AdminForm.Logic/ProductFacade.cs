using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain.Repository;
using TodoPagos.ProductImporterLogic;

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

        public void AddProduct(Product productToBeAdded)
        {
            if (!AlreadyInRepository(productToBeAdded))
            {
                unitOfWork.ProductsRepository.Insert(productToBeAdded);
                unitOfWork.Save();
            }
        }

        private bool AlreadyInRepository(Product productToBeAdded)
        {
            IEnumerable<Product> allProducts = unitOfWork.ProductsRepository.Get(null, null, "");
            foreach(Product product in allProducts)
            {
                if (product.Equals(productToBeAdded))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Product> GetProducts()
        {
            return unitOfWork.ProductsRepository.Get(null, null, "");
        }

    }
}
