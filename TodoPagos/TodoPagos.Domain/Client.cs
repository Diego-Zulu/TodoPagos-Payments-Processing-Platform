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
        int IDCard { get; set; }
        int PhoneNumber { get; set; }

        protected Client() { }

        public Client(string newName, int newIDCard, int newPhoneNumber)
        {
            Name = newName;
            IDCard = newIDCard;
            PhoneNumber = newPhoneNumber;
        }
    }
}
