using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TextField : IField
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Empty { get; set; }

        public TextField(string aName)
        {
            Name = aName;
            Empty = true;
        }

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullArgument(dataToBeFilledWith);
            TextField newTextField = new TextField(Name);
            newTextField.Data = dataToBeFilledWith;
            newTextField.Empty = false;
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
                return GetData().Equals(otherTextField.GetData()) &&
                    Name.Equals(otherTextField.Name);
            }
            catch (InvalidCastException)
            {
                return false;
            }
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
