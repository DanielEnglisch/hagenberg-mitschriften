using System;

namespace Common.Dal.Ado
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public Object Value { get; set; }
    }
}