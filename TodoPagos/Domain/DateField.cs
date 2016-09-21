using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DateField : IField
    {
        public DateTime Data { get; set; }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullOrNotValidDateTimeArgument(dataToBeFilledWith);
            DateField newDateField = new DateField();
            newDateField.Data = DateTime.ParseExact(dataToBeFilledWith, "d", null);
            return newDateField;
        }

        private void CheckForNullOrNotValidDateTimeArgument(string dataToBeFilledWith)
        {
            if (String.IsNullOrWhiteSpace(dataToBeFilledWith)) throw new ArgumentException();
            try
            {
                DateTime.ParseExact(dataToBeFilledWith, "d", null);
            }
            catch (FormatException)
            {
                throw new ArgumentException();
            }

        }

        public override string GetData()
        {
            return Data.ToShortDateString();
        }

        public override bool IsValid()
        {
            return Data.Year > 2013;
        }

        public override bool Equals(object otherIField)
        {
            DateField otherDateField = (DateField) otherIField;
            return GetData().Equals(otherDateField.GetData());
        }
    }
}
