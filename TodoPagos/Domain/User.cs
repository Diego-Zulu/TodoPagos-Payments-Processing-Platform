using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAddress = System.Net.Mail.MailAddress;

namespace Domain
{
    
    public class User
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual ICollection<Role> UserRoles { get; set; }

        private const int MINIMUM_ROLE_AMOUNT = 1;

        private User()
        {
            UserRoles = new List<Role>();
        }

        public User(string newUserName, string newUserEmail, Role newUserRole)
        {
            CheckAttributeCorrectness(newUserName, newUserEmail, newUserRole);
            Name = newUserName;
            Email = newUserEmail;
            UserRoles = new List<Role>();
            UserRoles.Add(newUserRole);
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
            return this.UserRoles.Contains(oneRole);
        }

        public void RemoveRole(Role oneRole)
        {
            if (UserRoles.Count == MINIMUM_ROLE_AMOUNT)
            {
                throw new InvalidOperationException();
            }
            UserRoles.Remove(oneRole);
        }

        public void AddRole(Role oneRole)
        {
            if (!UserRoles.Contains(oneRole))
            {
                UserRoles.Add(oneRole);
            }
        }

        public int GetRoleNumber()
        {
            return UserRoles.Count;
        }
    }
}
