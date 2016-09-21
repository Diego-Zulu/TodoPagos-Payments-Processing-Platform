using System;
using System.Collections.Generic;

namespace Domain
{
    public abstract class Role
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Privilege> Privileges { get; }

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

        private bool IsEqualRole(Role anotherRole)
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

        public bool HasPrivilege(Privilege onePrivilege)
        {
            return Privileges.Contains(onePrivilege);
        }
    }
}
