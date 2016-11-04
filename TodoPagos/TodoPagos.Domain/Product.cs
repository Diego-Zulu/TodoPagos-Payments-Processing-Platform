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
            MakeSureNeededInformationIsValid(newName, newDescription, newNeededPoints);
            Name = newName;
            Description = newDescription;
            NeededPoints = newNeededPoints;
        }

        private void MakeSureNeededInformationIsValid(string targetName, string targetDescription, int targetNeededPoints)
        {
            MakeSureTargetNameIsNotNullOrWhiteSpace(targetName);
            MakeSureTargetDescriptionIsNotNull(targetDescription);
            MakeSureTargetNeededPointsAreNotNegative(targetNeededPoints);
        }

        private void MakeSureTargetNameIsNotNullOrWhiteSpace(string targetName)
        {
            if (string.IsNullOrWhiteSpace(targetName))
            {
                throw new ArgumentException("El nombre de un producto no puede ser nulo o vacío");
            }
        }

        private void MakeSureTargetDescriptionIsNotNull(string targetDescription)
        {
            if (targetDescription == null)
            {
                throw new ArgumentException("La descripción de un producto puede ser vacía pero no nula");
            }
        }

        private void MakeSureTargetNeededPointsAreNotNegative(int targetNeededPoints)
        {
            if (targetNeededPoints < 0)
            {
                throw new ArgumentException("La cantidad de puntos necesarios para intercambiar un " 
                    + "producto no puede ser negativa");
            }
        }

        public void AddTargetQuantityToStock(int targetQuantity)
        {
            MakeSureStockWillNotBecomeNegative(this.Stock, targetQuantity);
            this.Stock = targetQuantity;
        }

        private void MakeSureStockWillNotBecomeNegative(int actualStock, int targetQuantity)
        {
            if (actualStock + targetQuantity < 0)
            {
                throw new ArgumentException("No se puede cambiar el stock de un producto para que el mismo quede negativo");
            }
        }

        public void UpdateName(string newName)
        {
            MakeSureTargetNameIsNotNullOrWhiteSpace(newName);
            this.Name = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            MakeSureTargetDescriptionIsNotNull(newDescription);
            this.Description = newDescription;
        }

        public void UpdateNeededPoints(int newNeededPoints)
        {
            MakeSureTargetNeededPointsAreNotNegative(newNeededPoints);
            this.NeededPoints = newNeededPoints;
        }
    }
}
