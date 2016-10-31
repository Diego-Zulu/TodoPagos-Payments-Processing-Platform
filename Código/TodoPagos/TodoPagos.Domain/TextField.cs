using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class TextField : IField
    {
        public string Data { get; set; }

        protected TextField() { }

        public TextField(string aName)
        {
            Name = aName;
            Data = null;
        }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullArgument(dataToBeFilledWith);
            TextField newTextField = new TextField(Name);
            newTextField.Data = dataToBeFilledWith;
            return newTextField;
        }

        private void CheckForNullArgument(string dataToBeFilledWith)
        {
            if (IsNull(dataToBeFilledWith)) throw new ArgumentException();
        }

        private bool IsNull(object anObject)
        {
            return anObject == null;
        }

        public override string GetData()
        {
            return Data;
        }

        public override bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Data);
        }

        public override bool Equals(object otherIField)
        {
            if (IsNull(otherIField)) return false;
            try
            {
                TextField otherTextField = (TextField)otherIField;

                return UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(otherTextField);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private bool UseCorrectComparationAcoordingIfTheyAreEmptyOrNot(TextField otherTextField)
        {
            if (this.IsEmpty() && otherTextField.IsEmpty())
            {
                return Name.Equals(otherTextField.Name);
            }
            else if (otherTextField.IsEmpty())
            {
                return false;

            }
            else
            {
                return GetData().Equals(otherTextField.GetData()) &&
                Name.Equals(otherTextField.Name);
            }
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
            IField clearedField = new TextField(this.Name);

            return clearedField;
        }
    }
}
