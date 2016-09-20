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
            if (dataToBeFilledWith == null) throw new ArgumentException();
            TextField newTextField = new TextField();
            newTextField.Data = dataToBeFilledWith;
            return newTextField;
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
