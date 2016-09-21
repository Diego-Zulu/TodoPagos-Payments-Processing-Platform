using System;
using System.Collections.Generic;

namespace Domain
{
    public class Provider
    {
        public long Commission { get; set; }

        public string Name { get; set; }

        public List<IField> Fields { get; set; } = new List<IField>();

        public Provider(string aName, long aCommission)
        {
            CheckForNegativeCommission(aCommission);
            Commission = aCommission;
            Name = aName;
        }

        public void ChangeCommission(long newValue)
        {
            CheckForNegativeCommission(newValue);
            Commission = newValue;
        }

        private void CheckForNegativeCommission(long newValue)
        {
            if (newValue < 0) throw new ArgumentException();
        }

        public void AddField(IField fieldToBeAdded)
        {
            Fields.Add(fieldToBeAdded);
        }

        public bool ContainsField(IField aField)
        {
            return Fields.Contains(aField);
        }

        public void RemoveField(IField fieldToBeRemoved)
        {
            Fields.Remove(fieldToBeRemoved);
        }
    }
}
