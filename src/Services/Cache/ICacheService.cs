using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Cache
{
    public interface ICacheService
    {
		void Put(int key, object value);

		object Get(int key);

		TValue Get<TValue>(int key) where TValue : class;

	}
}
