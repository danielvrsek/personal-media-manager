using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TestClassAttribute : Attribute
    {
    }
}
