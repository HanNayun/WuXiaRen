using System;

namespace UI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        public string ViewAddress { get; private set; }

        public ViewAttribute(string viewPrefabAddress)
        {
            ViewAddress = viewPrefabAddress;
        }
    }
}