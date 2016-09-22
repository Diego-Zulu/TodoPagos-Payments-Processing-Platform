using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DebitPayMethod : PayMethod
    {

        public override int PayAndReturnChange(int total)
        {
            throw new NotImplementedException();
        }
    }
}