using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductManager
    {
        private static ProductManager instance;

        static public ProductManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductManager();
            }

            return instance;
        }

    }
}
