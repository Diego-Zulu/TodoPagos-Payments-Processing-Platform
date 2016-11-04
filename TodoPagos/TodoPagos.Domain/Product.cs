using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NeededPoints { get; set; }
        public int Stock { get; set; }

        protected Product()
        {

        }

        public Product(string newName, string newDescription, int newNeededPoints)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("El nombre de un producto no puede ser nulo o vacío");
            }
            Name = newName;
            Description = newDescription;
            NeededPoints = newNeededPoints;
        }
    }
}
