using System;
using System.Collections.Generic;
using System.Text;

namespace NativeLibSourceGeneratorShared
{
    [AttributeUsage(AttributeTargets.Interface,AllowMultiple =true)]
    public class NativeLibAttribute : Attribute
    {
        public string LibName { get; private set; }
        public NativePlatformType[] Platform { get; private set; }    

        public NativeLibAttribute(string libName, params NativePlatformType[] platform)
        {
            LibName = libName;
            Platform = platform;
        }
    }

}
