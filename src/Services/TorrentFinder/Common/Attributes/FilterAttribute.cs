using System;
using System.Collections.Generic;
using System.Text;

namespace Services.TorrentFinder.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public abstract class FilterAttribute : Attribute
    {
		public object OnValueSetting(object value, Type valueType)
		{
			if (value == null)
			{
				return value;
			}

			switch (valueType)
			{
				case var t when valueType == typeof(string):
					return OnValueSetting(value as string);
				case var t when typeof(IEnumerable<string>).IsAssignableFrom(valueType):
					return OnValueSetting(value as IEnumerable<string>);
				default:
					throw new ArgumentException($"Type {valueType.Name} is not supported. " +
						"See the list of supported types of properties for more information.");
			}
		}

		public virtual string OnValueSetting(string value)
		{
			return value;
		}

		public virtual IEnumerable<string> OnValueSetting(IEnumerable<string> values)
		{
			return values;
		}
    }
}
