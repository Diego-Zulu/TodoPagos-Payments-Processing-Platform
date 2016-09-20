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

        public override IField FillAndClone(string dataToBeFilledWith)
        {
            CheckForNullArgument(dataToBeFilledWith);
            TextField newTextField = new TextField();
            newTextField.Data = dataToBeFilledWith;
            return newTextField;
        }

        private void CheckForNullArgument(string dataToBeFilledWith)
        {
            if (IsNull(dataToBeFilledWith)) throw new ArgumentException();
        }

        private bool IsNull(string dataToBeFilledWith)
        {
            return dataToBeFilledWith == null;
        }

        public override string GetData()
        {
            return Data;
        }

        public override bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Data);
        }
    }
}
