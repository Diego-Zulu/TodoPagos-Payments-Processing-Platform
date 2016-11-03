using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Client
    {
        int ID { get; set; }
        string Name { get; set; }
        string IDCard { get; set; }
        int PhoneNumber { get; set; }

        int MINIMUM_IDCARD_LENGTH = 7;
        int MAXIMUM_IDCARD_LENGTH = 8;

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
            if (string.IsNullOrWhiteSpace(targetIDCard) || !TargetIDCardHasValidVerificationDigit(targetIDCard))
            {
                throw new ArgumentException("La cédula del cliente no es válida");
            }
        }

        private bool TargetIDCardHasValidVerificationDigit(string targetIDCard)
        {
            int id;
            if (!int.TryParse(targetIDCard, out id) || id > 0 || targetIDCard.Length > MAXIMUM_IDCARD_LENGTH 
                || targetIDCard.Length < MINIMUM_IDCARD_LENGTH)
            {
                throw new ArgumentException();
            }

            if (targetIDCard.Length == MINIMUM_IDCARD_LENGTH)
            {
                targetIDCard = "0" + targetIDCard;
            }

            int checkSum = 0;
            for(int i=0; i<targetIDCard.Length - 1; i++)
            {
                checkSum += (targetIDCard[0] - '0') % 10;
            }

            checkSum = 10 - (checkSum % 10);

            return checkSum == (targetIDCard[targetIDCard.Length - 1] - '0');
        }
    }
}
