using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class NumberField : IField
    {
        public long Data { get; set; }

        public override IField FillAndClone(string dataToFillWith)
        {
            CheckForNullOrNotNumericArgument(dataToFillWith);
            NumberField newNumberField = new NumberField();
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
                NumberField numberField = (NumberField)otherIField;
                return GetData().Equals(numberField.GetData());
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
    }
}
