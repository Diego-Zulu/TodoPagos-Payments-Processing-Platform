using TodoPagos.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EmailAddress = System.Net.Mail.MailAddress;

namespace TodoPagos.UserAPI
{

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public int ID { get; set; }

        [NotMapped]
        private const int MINIMUM_ROLE_AMOUNT = 1;

        [NotMapped]
        private const int MIN_PASSWORD_LENGTH = 8;

        public User()
        {
            Roles = new List<Role>();
        }

        public User(User toBeCopiedUser)
        {
            if (toBeCopiedUser.IsComplete())
            {
                ID = toBeCopiedUser.ID;
                Name = toBeCopiedUser.Name;
                Email = toBeCopiedUser.Email;
                Roles = toBeCopiedUser.Roles;
                Salt = toBeCopiedUser.Salt;
                Password = toBeCopiedUser.Password;
            } else
            {
                throw new ArgumentException();
            }
        }

        public User(string newUserName, string newUserEmail, string newPassword, Role newUserRole)
        {
            CheckAttributeCorrectness(newUserName, newUserEmail, newPassword, newUserRole);
            Name = newUserName;
            Email = newUserEmail;
            Roles = new List<Role>();
            Roles.Add(newUserRole);
            DealWithPassword(newPassword);
        }

        public User(string newUserName, string newUserEmail, string newPassword, ICollection<Role> newRolesList)
        {
            CheckAttributeCorrectness(newUserName, newUserEmail, newPassword, newRolesList);
            Name = newUserName;
            Email = newUserEmail;
            Roles = newRolesList;
            DealWithPassword(newPassword);
        }

        private void DealWithPassword(string pass)
        {
            Salt = Hashing.GetRandomSalt();
            this.Password = Hashing.HashValue(pass, this.Salt);
        }

        private void CheckAttributeCorrectness(string newUserName, string newUserEmail, string newPassword, Role newUserRole)
        {
            CheckIfNameAndEmailAreCorrect(newUserName, newUserEmail);
            CheckIfRoleIsNotNull(newUserRole);
            CheckIfPasswordHasCorrectFormat(newPassword);
        }

        private void CheckAttributeCorrectness(string newUserName, string newUserEmail, string newPassword, ICollection<Role> newUserRoles)
        {
            CheckIfNameAndEmailAreCorrect(newUserName, newUserEmail);
            CheckIfRolesListIsNotNullOrEmpty(newUserRoles);
            CheckIfNoRoleIsNull(newUserRoles);
            CheckIfPasswordHasCorrectFormat(newPassword);
        }

        private void CheckIfRolesListIsNotNullOrEmpty(ICollection<Role> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfNoRoleIsNull(ICollection<Role> roles)
        {
            foreach (Role oneRole in roles)
            {
                if (oneRole == null)
                {
                    throw new ArgumentException();
                }
            }
        }

        private void CheckIfRoleIsNotNull(Role oneRole)
        {
            if (oneRole == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfPasswordHasCorrectFormat(string newPassword)
        {
            CheckForNullOrWhitespacePassword(newPassword);
            CheckForCorrectLengthPassword(newPassword);
            CheckForSafePassword(newPassword);
        }

        private void CheckForNullOrWhitespacePassword(string newPassword)
        {
            if (String.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException();
            }
        }

        private void CheckForCorrectLengthPassword(string newPassword)
        {
            if (newPassword.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException();
            }
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

        public void UpdateInfoWithTargetUsersInfo(User targetUser)
        {
            UpdateNameIfTargetUsersNameIsValid(targetUser);
            UpdateEmailIfTargetUsersEmailIsValid(targetUser);
            UpdateRolesIfTargetUsersRolesAreValid(targetUser);
            UpdatePasswordIfPasswordIsCorrect(targetUser);
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

        public void UpdatePasswordIfPasswordIsCorrect(User targetUser)
        {
            if (IsPasswordInCorrectFormat(targetUser.Password))
            {
                this.DealWithPassword(targetUser.Password);
            }
        }

        private bool IsPasswordInCorrectFormat(string aPassword)
        {
            try
            {
                CheckIfPasswordHasCorrectFormat(aPassword);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool HasPassword()
        {
            return Password != null;
        }

        public void ClearPassword()
        {
            this.Password = null;
        }

        public bool HasSalt()
        {
            return Salt != null;
        }

        public void ClearSalt()
        {
            this.Salt = null;
        }

        public void HashPasswordIfCorrect()
        {
            if (IsPasswordInCorrectFormat(this.Password))
            {
                DealWithPassword(this.Password);
            } else
            {
                CheckIfPasswordSeemsHashed(this.Password);
            }
            
        }

        public override bool Equals(object obj)
        {
            User castedObject = obj as User;
            if (castedObject != null)
            {
                return this.ID == castedObject.ID || this.Email.Equals(castedObject.Email);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsComplete()
        {
            try
            {
                DoChecksToSeeIfUserIsComplete();

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private void DoChecksToSeeIfUserIsComplete()
        {
            CheckIfNameAndEmailAreCorrect(this.Name, this.Email);
            CheckIfRolesListIsNotNullOrEmpty(this.Roles);
            CheckIfNoRoleIsNull(this.Roles);
            CheckIfPasswordHasCorrectFormatOrIsHashed(this.Password);
        }

        private void CheckIfPasswordHasCorrectFormatOrIsHashed(string aPass)
        {
            if (!IsPasswordInCorrectFormat(aPass))
            {
                CheckIfPasswordSeemsHashed(aPass);
            }
        }

        private void CheckIfPasswordSeemsHashed(string aPass)
        {
            string saltFromPassword = Hashing.GetSaltFromPassword(aPass);

            if (!Hashing.BothAreSaltsAndAreEqual(saltFromPassword, this.Salt))
            {
                throw new ArgumentException();
            }
        }

        public User CloneAndReturnNewUserWithoutPasswordAndSalt()
        {
            User newInstance = new User(this);

            newInstance.ClearPassword();
            newInstance.ClearSalt();

            return newInstance;
        }
    }
}
