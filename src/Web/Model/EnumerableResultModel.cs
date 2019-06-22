using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class EnumerableResultModel<TModel> : RequestResult
    {
		public IEnumerable<TModel> Results { get; set; }
    }
}
