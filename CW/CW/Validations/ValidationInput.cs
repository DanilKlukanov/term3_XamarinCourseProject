using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CW.Validations
{
    public class ValidationInput
    {
        public string Value { get; set; }
        public bool Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return false;
            }
            if (double.Parse(Value) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
