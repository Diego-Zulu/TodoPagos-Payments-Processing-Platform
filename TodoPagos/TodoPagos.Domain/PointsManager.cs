using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class PointsManager
    {
        public int ID { get; set; }
        public double MoneyPerPoint { get; set; }
        public virtual ICollection<Provider> Blacklist { get; set; }

        static private PointsManager instance;

        protected PointsManager()
        {
            Blacklist = new List<Provider>();
        }

        static public PointsManager GetInstance()
        {
            if (instance == null)
            {
                instance = new PointsManager();
                instance.MoneyPerPoint = 150;
            }

            return instance;
        }

        public void AddProviderToBlacklist(Provider targetProvider)
        {
            if (!this.Blacklist.Contains(targetProvider))
            {
                this.Blacklist.Add(targetProvider);
            }
        }

        public void RemoveProviderFromBlacklist(Provider blacklistedProvider)
        {
            if (this.Blacklist.Contains(blacklistedProvider))
            {
                this.Blacklist.Remove(blacklistedProvider);
            } else
            {
                throw new ArgumentException("Este proveedor no está en la lista negra");
            }
        }

        public void ChangeMoneyPerPointRatio(double newRatio)
        {
            if (newRatio < 0)
            {
                throw new ArgumentException("El ratio de dinero a puntos no puede ser negativo");
            }
            MoneyPerPoint = newRatio;
        }

        public bool AddPointsToClientIfProviderIsNotBlacklisted(double paidMoney, 
            Client clientWhoBought, Provider providerFromReceipt)
        {
            if (!this.Blacklist.Contains(providerFromReceipt))
            {
                int newPoints = (int) (paidMoney / MoneyPerPoint);
                clientWhoBought.AddPoints(newPoints);
                return true;
            }
            return false;
        }
    }
}
