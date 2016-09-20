using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class NumberField : IField
    {
        public int Data { get; set; }

        public override IField FillAndClone(string dataToFillWith)
        {
            throw new NotImplementedException();
        }

        public override string GetData()
        {
            return Data.ToString();
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
