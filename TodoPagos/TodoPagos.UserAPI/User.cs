using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAddress = System.Net.Mail.MailAddress;

namespace TodoPagos.UserAPI
{
    
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public int ID { get; set; }

        private const int MINIMUM_ROLE_AMOUNT = 1;

        private const int MIN_PASSWORD_LENGTH = 8;

        public User()
        {
            Roles = new List<Role>();
        }

        public User(string newUserName, string newUserEmail, string newPassword, Role newUserRole)
        {
            CheckAttributeCorrectness(newUserName, newUserEmail, newPassword, newUserRole);
            Name = newUserName;
            Email = newUserEmail;
            Password = newPassword;
            Roles = new List<Role>();
            Roles.Add(newUserRole);
        }

        private void CheckAttributeCorrectness(string newUserName, string newUserEmail, string newPassword, Role newUserRole)
        {
            CheckIfNameAndEmailAreCorrect(newUserName, newUserEmail);
            CheckIfRoleIsNotNull(newUserRole);
            CheckIfPasswordIsCorrect(newPassword);
        }

        private void CheckIfRoleIsNotNull(Role oneRole)
        {
            if (oneRole == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfPasswordIsCorrect(string newPassword)
        {
            CheckForNullOrWhitespacePassword(newPassword);
            CheckForCorrectLengthPassword(newPassword);
            CheckForSafePassword(newPassword);
        }

        private void CheckForNullOrWhitespacePassword(string newPassword)
        {
            if (String.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException();
        }

        private void CheckForCorrectLengthPassword(string newPassword)
        {
            if (newPassword.Length < MIN_PASSWORD_LENGTH) throw new ArgumentException();
        }

        private void CheckForSafePassword(string newPassword)
        {
            char[] password = newPassword.ToCharArray();
            bool hasNumber = false;
            bool hasUppercaseLetter = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 48 && password[i] <= 57) hasNumber = true;
                else if (password[i] >= 65 && password[i] <= 90) hasUppercaseLetter = true;
            }
            if (!(hasNumber && hasUppercaseLetter)) throw new ArgumentException();
        }

        private void CheckIfNameAndEmailAreCorrect(string aName, string anEmail)
        {
            if (IsValidName(aName) || NotValidEmail(anEmail))
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

        public bool IsComplete()
        {
            try
            {
                DoChecksToSeeIfUserIsComplete();

                return true;
            } catch (ArgumentException)
            {
                return false;
            }
        }

        private void DoChecksToSeeIfUserIsComplete()
        {
            CheckIfNameAndEmailAreCorrect(this.Name, this.Email);
            CheckIfPasswordIsCorrect(this.Password);
            CheckIfHasAtLeastMinimumNumberOfRoles(this.Roles);
        }

        private void CheckIfHasAtLeastMinimumNumberOfRoles(ICollection<Role> rolesList)
        {
            if (!IsValidRolesList(rolesList))
            {
                throw new ArgumentException();
            }
        }

        public void UpdateInfoWithTargetsUserInfo(User targetUser)
        {
            UpdateNameIfTargetUsersNameIsValid(targetUser);
            UpdateEmailIfTargetUsersEmailIsValid(targetUser);
            UpdatePasswordIfTargetUsersPasswordIsValid(targetUser);
            UpdateRolesIfTargetUsersRolesAreValid(targetUser);
        }

        private void UpdateNameIfTargetUsersNameIsValid(User targetUser)
        {
            if (IsValidName(targetUser.Name))
            {
                this.Name = targetUser.Name;
            }
        }

        private void UpdateEmailIfTargetUsersEmailIsValid(User targetUser)
        {
            if (!NotValidEmail(targetUser.Email))
            {
                this.Email = targetUser.Email;
            }
        }

        private void UpdatePasswordIfTargetUsersPasswordIsValid(User targetUser)
        {
            if (IsValidPassword(targetUser.Password))
            {
                this.Password = targetUser.Password;
            }
        }

        private void UpdateRolesIfTargetUsersRolesAreValid(User targetUser)
        {
            if (IsValidRolesList(targetUser.Roles))
            {
                this.Roles.Clear();
                this.Roles.Concat(targetUser.Roles);
            }
        }

        private bool IsValidRolesList(ICollection<Role> rolesList)
        {
            return rolesList != null && rolesList.Count >= MINIMUM_ROLE_AMOUNT;
        }

        private bool IsValidName(string aName)
        {
            return String.IsNullOrWhiteSpace(aName);
        }

        private bool IsValidPassword(string aPassword)
        {
            try
            {
                CheckIfPasswordIsCorrect(aPassword);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
