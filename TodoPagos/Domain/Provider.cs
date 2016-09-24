using System;
using System.Collections.Generic;

namespace Domain
{
    public class Provider
    {
        public double Commission { get; set; }

        public string Name { get; set; }

        public List<IField> Fields { get; set; } = new List<IField>();

        public Provider(string aName, double aCommission)
        {
            CheckForNegativeCommission(aCommission);
            Commission = aCommission;
            Name = aName;
        }

        public Provider(string aName, double aCommission, List<IField> fields)
        {
            CheckForPossibleErrors(fields, aCommission);
            Commission = aCommission;
            Name = aName;
            Fields = fields;
        }

        private void CheckForPossibleErrors(List<IField> fields, double aCommission)
        {
            CheckForNullFieldsList(fields);
            CheckForNegativeCommission(aCommission);
        }

        private void CheckForNullFieldsList(List<IField> fields)
        {
            if(IsNull(fields))
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckForNegativeCommission(double newValue)
        {
            if (newValue < 0) throw new ArgumentException();
        }

        public void ChangeCommission(double newValue)
        {
            CheckForNegativeCommission(newValue);
            Commission = newValue;
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
            CheckIfFieldIsContainedInFieldsList(fieldToBeRemoved);
            Fields.Remove(fieldToBeRemoved);
        }

        private void CheckIfFieldIsContainedInFieldsList(IField fieldToBeRemoved)
        {
            if (!ContainsField(fieldToBeRemoved)) throw new ArgumentException();
        }

        public override bool Equals(object anotherProvider)
        {
            if (IsNull(anotherProvider)) return false;
            try
            {
                Provider otherProvider = (Provider)anotherProvider;
                return Name.Equals(otherProvider.Name);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private bool IsNull(object anObject)
        {
            return anObject == null;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
