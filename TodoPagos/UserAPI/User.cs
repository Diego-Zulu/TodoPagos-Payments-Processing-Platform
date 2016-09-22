using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAddress = System.Net.Mail.MailAddress;

namespace UserAPI
{
    
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        private const int MINIMUM_ROLE_AMOUNT = 1;

        private User()
        {
            Roles = new List<Role>();
        }

        public User(string newUserName, string newUserEmail, Role newUserRole)
        {
            CheckAttributeCorrectness(newUserName, newUserEmail, newUserRole);
            Name = newUserName;
            Email = newUserEmail;
            Roles = new List<Role>();
            Roles.Add(newUserRole);
        }

        private void CheckAttributeCorrectness(string newUserName, string newUserEmail, Role newUserRole)
        {
            CheckIfNameAndEmailAreCorrect(newUserName, newUserEmail);
            CheckIfRoleIsNotNull(newUserRole);
        }

        private void CheckIfRoleIsNotNull(Role oneRole)
        {
            if (oneRole == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfNameAndEmailAreCorrect(string aName, string anEmail)
        {
            if (String.IsNullOrWhiteSpace(aName) || NotValidEmail(anEmail))
            {
                throw new ArgumentException();
            }
        }

        private bool NotValidEmail(string anEmail)
        {
            try
            {
                EmailAddress parsedAddress = new EmailAddress(anEmail);
                return !anEmail.Equals(parsedAddress.Address);
            }
            catch (FormatException)
            {
                return true;
            }
            catch (ArgumentNullException)
            {
                return true;
            }
        }

        public bool HasThisRole(Role oneRole)
        {
            return this.Roles.Contains(oneRole);
        }

        public void RemoveRole(Role oneRole)
        {
            if (Roles.Count == MINIMUM_ROLE_AMOUNT)
            {
                throw new InvalidOperationException();
            }
            Roles.Remove(oneRole);
        }

        public void AddRole(Role oneRole)
        {
            if (!Roles.Contains(oneRole))
            {
                Roles.Add(oneRole);
            }
        }

        public int GetRoleCount()
        {
            return Roles.Count;
        }

        public bool HasPrivilege(Privilege onePrivilege)
        {
            bool hasPrivilege = false;
            for (int index = 0; index < Roles.Count && !hasPrivilege; index++)
            {
                if (Roles.ElementAt(index).HasPrivilege(onePrivilege))
                {
                    hasPrivilege = true;
                }
            }
            return hasPrivilege;
        }
    }
}
