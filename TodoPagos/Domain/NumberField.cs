﻿using System;
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
            NumberField newNumberField = new NumberField();
            newNumberField.Data = Int32.Parse(dataToFillWith);
            return newNumberField;
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
