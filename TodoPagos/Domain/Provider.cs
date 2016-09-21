using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Provider
    {
        public long Commission { get; set; }

        public string Name { get; set; }

        public Provider(string aName, long aCommission)
        {
            Commission = aCommission;
            Name = aName;
        }

        public void ChangeCommission(long newValue)
        {
            CheckForNegativeCommission(newValue);
            Commission = newValue;
        }

        private void CheckForNegativeCommission(long newValue)
        {
            if (newValue < 0) throw new ArgumentException();
        }
    }
}
