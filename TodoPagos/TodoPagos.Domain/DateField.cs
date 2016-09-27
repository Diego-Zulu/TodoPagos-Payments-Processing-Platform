using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class DateField : IField
    {
        public DateTime Data { get; set; }

        public string Name { get; set; }

        public bool Empty { get; set; }

        public DateField(string aName)
        {
            Name = aName;
            Empty = true;
        }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullOrNotValidDateTimeArgument(dataToBeFilledWith);
            DateField newDateField = new DateField(Name);
            newDateField.Data = DateTime.Parse(dataToBeFilledWith);
            newDateField.Empty = false;
            return newDateField;
        }

        private void CheckForNullOrNotValidDateTimeArgument(string dataToBeFilledWith)
        {
            if (String.IsNullOrWhiteSpace(dataToBeFilledWith)) throw new ArgumentException();
            try
            {
                DateTime.Parse(dataToBeFilledWith);
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
            if (IsNull(otherIField)) return false;
            try
            {
                DateField otherDateField = (DateField)otherIField;
                return GetData().Equals(otherDateField.GetData()) &&
                    Name.Equals(otherDateField.Name);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private bool IsNull(object anObject)
        {
            return anObject == null;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public override bool IsEmpty()
        {
            return Empty;
        }
    }
}
