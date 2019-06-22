using Services.TorrentFinder.Common.Attributes;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Services.TorrentFinder.Common.Extensions
{
	internal static class RegexParserExtensions
	{
		public static string GetPropertyPattern<TValue>(this Type type, Expression<Func<TValue>> expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null)
			{
				throw new ArgumentException("Expression must be of a type MemberExpression");
			}

			string name = memberExpression.Member.Name;

			return type.GetPropertyPattern(name);
		}

		public static string GetPropertyPattern(this Type type, string propertyName)
		{
			return type.GetProperty(propertyName).GetPropertyPattern();
		}

		public static string GetPropertyPattern(this PropertyInfo propertyInfo)
		{
			PropertyPatternAttribute propertyPatternAttribute = null;
			bool ignore = false;

			foreach (Attribute customAttribute in propertyInfo.GetCustomAttributes())
			{
				switch (customAttribute)
				{
					case ParseIgnoreAttribute p:
						ignore = true;
						break;
					case PropertyPatternAttribute p:
						propertyPatternAttribute = p;
						break;
				}
			}

			if (propertyPatternAttribute == null && !ignore)
			{
				throw new InvalidOperationException($"Type '{propertyInfo.ReflectedType.Name}' does not contain property '{propertyInfo.Name}' with attribute 'PropertyPatternAttribute'.");
			}

			return ignore ? null : propertyPatternAttribute.Pattern;
		}
	}
}
