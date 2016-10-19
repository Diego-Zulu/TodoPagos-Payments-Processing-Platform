using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class DateField : IField
    {
        private readonly string[] ACCEPTED_DATE_FORMATS = new[]{"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'",
                    "ddd, d MMM yyyy HH':'mm':'ss 'GMT'"};
        public virtual DateTime Data { get; set; }

        public string Name { get; set; }

        public bool Empty { get; set; }

        protected DateField() { }

        public DateField(string aName)
        {
            Name = aName;
            Empty = true;
        }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullOrNotValidDateTimeArgument(dataToBeFilledWith);
            DateField newDateField = new DateField(Name);
            newDateField.Data = ParseToGMTDate(dataToBeFilledWith);
            newDateField.Empty = false;
            return newDateField;
        }

        private void CheckForNullOrNotValidDateTimeArgument(string dataToBeFilledWith)
        {
            if (String.IsNullOrWhiteSpace(dataToBeFilledWith)) throw new ArgumentException();
            try
            {
                ParseToGMTDate(dataToBeFilledWith);
            }
            catch (FormatException)
            {
                throw new ArgumentException();
            }

        }

        private DateTime ParseToGMTDate(string dataToBeFilledWith)
        {
            return DateTime.ParseExact(dataToBeFilledWith, ACCEPTED_DATE_FORMATS,
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public override string GetData()
        {
            return Data.ToShortDateString();
        }

        public override bool IsValid()
        {
            return true;
        }

        public override bool Equals(object otherIField)
        {
            if (IsNull(otherIField)) return false;
            try
            {
                DateField otherDateField = (DateField)otherIField;
                return UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(otherDateField);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        private bool UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(DateField otherDateField)
        {
            if (this.IsEmpty() && otherDateField.IsEmpty())
            {
                return Name.Equals(otherDateField.Name);
            }
            else if (otherDateField.IsEmpty())
            {
                return false;

            }
            else
            {
                return GetData().Equals(otherDateField.GetData()) &&
                Name.Equals(otherDateField.Name);
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

        public override IField ClearDataAndClone()
        {
            IField clearedField = new DateField(this.Name);

            return clearedField;
        }
    }
}
