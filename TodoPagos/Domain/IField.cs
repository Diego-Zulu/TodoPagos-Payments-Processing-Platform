using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class IField
    {
        public abstract bool IsValid();

        public abstract IField FillAndClone(string dataToBeFilledWith);

        public abstract string GetData();
    }
}
