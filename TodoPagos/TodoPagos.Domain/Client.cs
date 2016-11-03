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

        private int MINIMUM_IDCARD_LENGTH = 7;
        private int MAXIMUM_IDCARD_LENGTH = 8;
        private int[] NUMBERS_TO_MULTIPLY_IDCARD_WITH = { 2, 9, 8, 7, 6, 3, 4 };

        protected Client() { }

        public Client(string newName, string newIDCard, int newPhoneNumber)
        {
            MakeSureTargetNameIsNotNullOrWhiteSpace(newName);
            MakeSureTargetIDCardIsValid(newIDCard);
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
                checkSum += (NUMBERS_TO_MULTIPLY_IDCARD_WITH[i] * (targetIDCard[i] - '0')) % 10;
            }

            return 10 - (checkSum % 10);
        }
    }
}
