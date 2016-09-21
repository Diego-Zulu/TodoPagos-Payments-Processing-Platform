using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI
{
    public abstract class Privilege
    {
        public virtual string Name { get; set; }

        public override bool Equals(Object anObject)
        {
            try
            {
                Privilege castedObject = (Privilege)anObject;
                return this.IsEqualPrivilege(castedObject);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        public virtual bool IsEqualPrivilege(Privilege anotherPrivilege)
        {
            bool isEqualPrivilege = false;
            if (anotherPrivilege != null)
            {
                isEqualPrivilege = this.Name.Equals(anotherPrivilege.Name);
            }
            return isEqualPrivilege;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
