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
            throw new NotImplementedException();
        }

        public override string GetData()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
