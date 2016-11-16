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
        private readonly string[] ACCEPTED_DATE_FORMATS = new[] { "yyyy-MM-ddTHH:mm:ssZ" };
        public virtual DateTime? Data { get; set; }

        protected DateField() {
            Type = "DateField";
        }

        public DateField(string aName)
        {
            Type = "DateField";
            Name = aName;
            Data = null;
        }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullOrNotValidDateTimeArgument(dataToBeFilledWith);
            DateField newDateField = new DateField(Name);
            newDateField.Data = ParseToISO8061Date(dataToBeFilledWith);

            return newDateField;
        }

        private void CheckForNullOrNotValidDateTimeArgument(string dataToBeFilledWith)
        {
            if (String.IsNullOrWhiteSpace(dataToBeFilledWith)) throw new ArgumentException();
            try
            {
                ParseToISO8061Date(dataToBeFilledWith);
            }
            catch (FormatException)
            {
                throw new ArgumentException("El argumento es nulo o no válido");
            }

        }

        private DateTime ParseToISO8061Date(string dataToBeFilledWith)
        {
            return DateTime.ParseExact(dataToBeFilledWith, ACCEPTED_DATE_FORMATS,
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public override string GetData()
        {
            if (Data == null)
            {
                return null;
            }
            return Data.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
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
            return Data == null;
        }

        public override IField ClearDataAndClone()
        {
            IField clearedField = new DateField(this.Name);

            return clearedField;
        }
    }
}
