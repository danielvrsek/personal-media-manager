using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class RequestResult
    {
		public bool IsSuccess { get; set; } = true;

		public string ErrorMessage { get; set; }
    }
}
