using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AddressableAttribute : Attribute
    {
        public string Address { get; private set; }

        public AddressableAttribute(string address)
        {
            Address = address;
        }
    }
}