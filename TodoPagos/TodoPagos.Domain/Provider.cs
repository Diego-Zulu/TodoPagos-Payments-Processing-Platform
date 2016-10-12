﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.Domain
{
    public class Provider
    {
        public int ID { get; set; }
        public double Commission { get; set; }

        public string Name { get; set; }

        public ICollection<IField> Fields { get; set; } = new List<IField>();

        public bool Active { get; set; }

        public Provider()
        {

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
            if (String.IsNullOrWhiteSpace(aName)) throw new ArgumentException();
        }

        private void CheckForCompleteField(ICollection<IField> fields)
        {
            foreach (IField oneField in fields)
            {
                if (!oneField.IsEmpty())
                {
                    throw new ArgumentException();
                }
            }
        }

        private void CheckForMoreThan100Comission(double aCommission)
        {
            if (aCommission > 100)
            {
                throw new ArgumentException();
            }
        }

        private void CheckForNullFieldsList(ICollection<IField> fields)
        {
            if(IsNull(fields))
            {
                throw new ArgumentException();
            }
        }

        private void CheckForNegativeCommission(double newValue)
        {
            if (newValue < 0) throw new ArgumentException();
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
            if (!ContainsField(fieldToBeRemoved)) throw new ArgumentException();
        }

        public override bool Equals(object anotherProvider)
        {
            if (IsNull(anotherProvider)) return false;
            try
            {
                Provider otherProvider = (Provider)anotherProvider;
                return Name.Equals(otherProvider.Name) || ID == otherProvider.ID;
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

        public bool IsComplete()
        {
            try
            {
                CheckForPossibleErrors(this.Fields, this.Commission, this.Name);
                return true;
            } catch (ArgumentException)
            {
                return false;
            }       
        }
    }
}
