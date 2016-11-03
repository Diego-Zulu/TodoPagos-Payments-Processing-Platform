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

        public PointsManager()
        {
            MoneyPerPoint = 150;
            Blacklist = new List<Provider>();
        }
    }
}
