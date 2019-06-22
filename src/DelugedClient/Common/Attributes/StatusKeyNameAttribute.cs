using System;

namespace DelugedClient.Common
{
	internal class StatusKeyNameAttribute : Attribute
    {
		public string Name { get; }

		public StatusKeyNameAttribute(string name)
		{
			this.Name = name;
		}
    }
}
