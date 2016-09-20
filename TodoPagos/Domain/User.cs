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
        string Name { get; set; }
        string Email { get; set; }

        public User(string newUserName, string newUserEmail)
        {
            CheckIfNameAndEmailAreCorrect(newUserName, newUserEmail);
            Name = newUserName;
            Email = newUserEmail;

        }

        private void CheckIfNameAndEmailAreCorrect(string aName, string anEmail)
        {
            if (String.IsNullOrWhiteSpace(aName) || NotValidEmail(anEmail))
            {
                throw new ArgumentException("Not valid Email");
            }
        }

        private bool NotValidEmail(string anEmail)
        {
            try
            {
                EmailAddress parsedAddress = new System.Net.Mail.MailAddress(anEmail);
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
    }
}
