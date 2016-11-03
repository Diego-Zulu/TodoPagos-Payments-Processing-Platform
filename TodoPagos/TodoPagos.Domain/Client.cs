using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public int PhoneNumber { get; set; }
        public int Points { get; set; }

        private int MINIMUM_IDCARD_LENGTH = 7;
        private int MAXIMUM_IDCARD_LENGTH = 8;
        private int[] NUMBERS_TO_MULTIPLY_IDCARD_WITH = { 2, 9, 8, 7, 6, 3, 4 };

        private int HOUSE_PHONE_LENGTH = 8;
        private int MOBILE_PHONE_LENGTH = 9;

        protected Client() { }

        public Client(string newName, string newIDCard, int newPhoneNumber)
        {
            MakeSureTargetNameIsNotNullOrWhiteSpace(newName);
            MakeSureTargetIDCardIsValid(newIDCard);
            MakeSureTargetPhoneNumberIsValid(newPhoneNumber);
            Name = newName.Trim();
            IDCard = newIDCard.Trim();
            PhoneNumber = newPhoneNumber;
        }

        private void MakeSureTargetNameIsNotNullOrWhiteSpace(string targetName)
        {
            if (string.IsNullOrWhiteSpace(targetName))
            {
                throw new ArgumentException("El nombre de un cliente no puede ser vacío");
            }
        }

        private void MakeSureTargetPhoneNumberIsValid(int targetPhoneNumber)
        {
            string targetPhoneNumberInString = targetPhoneNumber + "";
            if (targetPhoneNumber < 0 || !TargetPhoneNumberSeemsValid(targetPhoneNumberInString))
            {
                throw new ArgumentException("El número de teléfono del cliente no es válido");
            }
        }

        private bool TargetPhoneNumberSeemsValid(string targetPhoneNumber)
        {
            if (targetPhoneNumber.Length == MOBILE_PHONE_LENGTH)
            {
                return targetPhoneNumber[0] == '0' && targetPhoneNumber[1] == '9'; 
            }

            return targetPhoneNumber.Length == HOUSE_PHONE_LENGTH;
        }

        private void MakeSureTargetIDCardIsValid(string targetIDCard)
        {
            MakeSureTargetIDCardHasValidFormat(targetIDCard);
            MakeSureTargetIDCardHasValidVerificationDigit(targetIDCard);
        }

        private void MakeSureTargetIDCardHasValidVerificationDigit(string targetIDCard)
        {
            if (!TargetIDCardHasValidVerificationDigit(targetIDCard))
            {
                throw new ArgumentException("El número verificador de la cédula del cliente no coincide con la misma");
            }
        }

        private void MakeSureTargetIDCardHasValidFormat(string targetIDCard)
        {
            int id;
            if (string.IsNullOrWhiteSpace(targetIDCard) || !int.TryParse(targetIDCard, out id) || id < 0 
                || targetIDCard.Length > MAXIMUM_IDCARD_LENGTH || targetIDCard.Length < MINIMUM_IDCARD_LENGTH)
            {
                throw new ArgumentException("La cédula del cliente no tiene formato válido");
            }
        }

        private bool TargetIDCardHasValidVerificationDigit(string targetIDCard)
        {
            targetIDCard = Add0ToTheStartOfTheIDCardIfLengthDemandsIt(targetIDCard);

            int checkSum = CalculateCheckSumToCompareWithVerificationDigit(targetIDCard);

            return checkSum == (targetIDCard[targetIDCard.Length - 1] - '0');
        }

        private string Add0ToTheStartOfTheIDCardIfLengthDemandsIt(string targetIDCard)
        {
            if (targetIDCard.Length == MINIMUM_IDCARD_LENGTH)
            {
                targetIDCard = "0" + targetIDCard;
            }
            return targetIDCard;
        }

        private int CalculateCheckSumToCompareWithVerificationDigit(string targetIDCard)
        {
            int checkSum = 0;
            for (int i = 0; i < targetIDCard.Length - 1; i++)
            {
                checkSum += NUMBERS_TO_MULTIPLY_IDCARD_WITH[i] * (targetIDCard[i] - '0');
            }
            checkSum = checkSum % 10;

            return (10 - checkSum) % 10;
        }

        public override bool Equals(object obj)
        {
            Client objAsClient = obj as Client;

            if (objAsClient == null)
            {
                return false;
            }

            return objAsClient.ID == this.ID || object.Equals(objAsClient.IDCard, this.IDCard);
        }

        //public override int GetHashCode()
        //{
        //    return IDCard.GetHashCode();
        //}
    }
}
