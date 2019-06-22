using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Cache
{
    public class CacheService : ICacheService
    {
		private Dictionary<int, object> _cache = new Dictionary<int, object>();
		private object _cacheLock = new object();

		public void Put(int key, object value)
		{
			lock (_cacheLock)
			{
				_cache.Add(key, value);
			}
		}

		public object Get(int key)
		{
			lock (_cacheLock)
			{
				return _cache.GetValueOrDefault(key);
			}
		}

		public TValue Get<TValue>(int key)
			where TValue: class
		{
			lock (_cacheLock)
			{
				return _cache.GetValueOrDefault(key) as TValue;
			}
		}
    }
}