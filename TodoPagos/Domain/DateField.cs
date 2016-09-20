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
            throw new NotImplementedException();
        }

        public override string GetData()
        {
            return Data.ToShortDateString();
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
