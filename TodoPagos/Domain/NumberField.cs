﻿using System;
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
            if (dataToFillWith == null) throw new ArgumentException();
            try
            {
                NumberField newNumberField = new NumberField();
                newNumberField.Data = long.Parse(dataToFillWith);
                return newNumberField;
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
    }
}
