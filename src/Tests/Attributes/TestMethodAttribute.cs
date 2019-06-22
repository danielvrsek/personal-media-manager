using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodAttribute : Attribute
    {
    }
}
