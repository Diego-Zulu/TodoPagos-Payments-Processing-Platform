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
        public ICollection<Provider> Blacklist { get; set; }

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
    }
}
