using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class IField
    {
        public abstract bool IsValid();

        public abstract IField FillAndClone(string dataToBeFilledWith);

        public abstract string GetData();

        public abstract bool IsEmpty();

        public abstract override bool Equals(object otherIField);

        public abstract override int GetHashCode();
    }
}
