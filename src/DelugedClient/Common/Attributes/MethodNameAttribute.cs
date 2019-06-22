using System;

namespace DelugedClient.Common
{
	internal class MethodNameAttribute : Attribute
    {
		public string Name { get; }

		public MethodNameAttribute(string name)
		{
			Name = name;
		}
    }
}
