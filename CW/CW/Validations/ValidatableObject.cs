using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace CW.Validations
{
    public class ValidatableObject<T> : INotifyPropertyChanged
    {
        public ValidatableObject()
        {
            _validations = new List<IValidationRule<T>>();
            _errors = new List<string>();
        }

        public T Value { get; set; }

        private List<IValidationRule<T>> _validations;
        public List<IValidationRule<T>> Validations => _validations;

        private List<string> _errors;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Errors
        {
            get => _errors;
            set
            {
                if (_errors != value)
                {
                    _errors = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Errors"));
                }
            }
        }

        public bool Validate()
        {
            Errors.Clear();

            var errors = _validations.Where(x => !x.Check(Value)).Select(x => x.ValidationMessage);
            Errors = errors.ToList();

            return !Errors.Any();
        }

        //private bool IsValid { get; set; }
    }
}
