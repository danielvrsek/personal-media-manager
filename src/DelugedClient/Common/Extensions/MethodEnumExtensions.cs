using DelugedClient.Model;
using Vrsek.Common.Extensions;

namespace DelugedClient.Common
{
    public static class MethodEnumExtensions
    {
		public static string GetMethodName(this Method value)
		{
			return value.GetCustomAttribute<MethodNameAttribute>().Name;
		}
	}
}
