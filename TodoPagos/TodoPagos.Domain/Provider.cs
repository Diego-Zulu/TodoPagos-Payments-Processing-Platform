using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.Domain
{
    public class Provider
    {
        public int ID { get; set; }

        public double Commission { get; set; }

        public string Name { get; set; }

        public virtual ICollection<IField> Fields { get; set; }

        public bool Active { get; set; }

        public Provider()
        {
            Fields = new List<IField>();
        }

        public Provider(string aName, double aCommission, ICollection<IField> fields)
        {
            CheckForPossibleErrors(fields, aCommission, aName);
            Commission = aCommission;
            Name = aName;
            Fields = fields;
            Active = true;
        }

        private void CheckForPossibleErrors(ICollection<IField> fields, double aCommission, string aName)
        {
            CheckForPossibleFieldsErrors(fields);
            CheckForPossibleCommissionErrors(aCommission);
            CheckForNullOrWhitespaceName(aName);
        }

        private void CheckForPossibleFieldsErrors(ICollection<IField> fields)
        {
            CheckForNullFieldsList(fields);
            CheckForCompleteField(fields);
        }

        private void CheckForNullOrWhitespaceName(string aName)
        {
            if (String.IsNullOrWhiteSpace(aName)) throw new ArgumentException("El nombre es nulo o whitespace");
        }

        private void CheckForCompleteField(ICollection<IField> fields)
        {
            foreach (IField oneField in fields)
            {
                if (!oneField.IsEmpty())
                {
                    throw new ArgumentException("Todos los campos deben estar completos");
                }
            }
        }

        private void CheckForMoreThan100Comission(double aCommission)
        {
            if (aCommission > 100)
            {
                throw new ArgumentException("La comisión debe ser menor o igual a 100%");
            }
        }

        private void CheckForNullFieldsList(ICollection<IField> fields)
        {
            if (IsNull(fields))
            {
                throw new ArgumentException("La lista de campos no puede ser nula");
            }
        }

        private void CheckForNegativeCommission(double newValue)
        {
            if (newValue < 0) throw new ArgumentException("La comisión no puede ser negativa");
        }

        public void ChangeCommission(double newValue)
        {
            CheckForPossibleCommissionErrors(newValue);
            Commission = newValue;
        }

        private void CheckForPossibleCommissionErrors(double aCommission)
        {
            CheckForMoreThan100Comission(aCommission);
            CheckForNegativeCommission(aCommission);
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
            if (!ContainsField(fieldToBeRemoved)) throw new ArgumentException("El campo a eliminar debe estar contenido " + 
                "en la lista de campos");
        }

        public override bool Equals(object anotherProvider)
        {
            if (IsNull(anotherProvider)) return false;
            try
            {
                Provider otherProvider = (Provider)anotherProvider;
                return object.Equals(Name, otherProvider.Name) || ID.Equals(otherProvider.ID);
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

        public void MarkAsInactiveToShowItIsDeleted()
        {
            this.Active = false;
        }

        public bool IsCompleteAndActive()
        {
            try
            {
                CheckForPossibleErrors(this.Fields, this.Commission, this.Name);
                return this.Active;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool IsCompletelyEqualTo(Provider anotherProvider)
        {
            return SeeIfAllAttributesButTheFieldsListsAreEqual(anotherProvider)
                && TheTwoFieldsListAreCompletelyEqual(Fields, anotherProvider.Fields);
        }

        private bool SeeIfAllAttributesButTheFieldsListsAreEqual(Provider anotherProvider)
        {
            return Name.Equals(anotherProvider.Name) && Commission.Equals(anotherProvider.Commission)
                 && ID == anotherProvider.ID && Active.Equals(anotherProvider.Active);
        }

        private bool TheTwoFieldsListAreCompletelyEqual(IEnumerable<IField> firstFieldsList, IEnumerable<IField> secondFieldsList)
        {
            return firstFieldsList.All(x => secondFieldsList.Contains(x)) && secondFieldsList.All(x => firstFieldsList.Contains(x));
        }

        public override string ToString()
        {
            return Name;
        }

        public bool AllTargetFieldsAndThisFieldsAreEqualNotRegardingData(ICollection<IField> someFields)
        {
            foreach (IField oneField in someFields)
            {
                IField oneEmptyField = oneField.ClearDataAndClone();
                if (!this.Fields.Contains(oneEmptyField))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
