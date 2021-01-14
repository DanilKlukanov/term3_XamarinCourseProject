using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CW.Validations
{
    public class IncorrectSymbolsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        private char[] _incorrectChars = { ' ' };

        public bool Check(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            var str = value as string;


            return _incorrectChars.Any(x => !str.Contains(x));
        }
    }
}
