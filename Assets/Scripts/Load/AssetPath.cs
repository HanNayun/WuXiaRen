using System;

namespace Load
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AssetPathAttribute : Attribute
    {
        public string Path { get; private set; }

        public AssetPathAttribute(string path)
        {
            Path = path;
        }
    }
}