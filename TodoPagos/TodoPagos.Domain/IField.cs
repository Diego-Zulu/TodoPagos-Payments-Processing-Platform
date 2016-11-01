using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public abstract class IField
    {
        public abstract bool IsValid();

        public string Name { get; set; }

        public abstract IField FillAndClone(string dataToBeFilledWith);

        public abstract string GetData();

        public abstract bool IsEmpty();

        public abstract override bool Equals(object otherIField);

        public abstract override int GetHashCode();

        public int ID { get; set; }

        public abstract IField ClearDataAndClone();
    }
}
