using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class NumberField : IField
    {
        public long? Data { get; set; }

        protected NumberField() { }

        public NumberField(string aName)
        {
            Name = aName;
            Data = null;
        }

        public override IField FillAndClone(string dataToFillWith)
        {
            CheckForNullOrNotNumericArgument(dataToFillWith);
            NumberField newNumberField = new NumberField(Name);
            newNumberField.Data = long.Parse(dataToFillWith);
            return newNumberField;
        }

        private void CheckForNullOrNotNumericArgument(string dataToFillWith)
        {
            if (String.IsNullOrEmpty(dataToFillWith)) throw new ArgumentException();
            try
            {
                long.Parse(dataToFillWith);
            }
            catch (FormatException)
            {
                throw new ArgumentException();
            }
        }

        public override string GetData()
        {
            if (Data == null)
            {
                return null;
            }
            return Data.ToString();
        }

        public override bool IsValid()
        {
            return Data > 0;
        }

        public override bool Equals(object otherIField)
        {
            if (IsNull(otherIField)) return false;
            try
            {
                NumberField otherNumberField = (NumberField)otherIField;
                return UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(otherNumberField);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        private bool UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(NumberField otherNumberField)
        {
            if (this.IsEmpty() && otherNumberField.IsEmpty())
            {
                return Name.Equals(otherNumberField.Name);
            }
            else if (otherNumberField.IsEmpty())
            {
                return false;

            }
            else
            {
                return GetData().Equals(otherNumberField.GetData()) &&
                Name.Equals(otherNumberField.Name);
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
            IField clearedField = new NumberField(this.Name);

            return clearedField;
        }
    }
}
