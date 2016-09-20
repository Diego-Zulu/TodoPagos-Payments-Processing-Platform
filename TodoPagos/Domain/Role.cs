using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Role
    {
        public virtual string Name { get; set; }

        public override bool Equals(Object anObject)
        {
            try {
                Role castedObject = (Role)anObject;
                return this.IsEqualRole(castedObject);
            } catch (System.InvalidCastException)
            {
                return false;
            }
        }

        public virtual bool IsEqualRole(Role anotherRole)
        {
            bool isEqualRole = false;
            if (anotherRole != null)
            {
                isEqualRole = this.Name.Equals(anotherRole.Name);
            }
            return isEqualRole;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
