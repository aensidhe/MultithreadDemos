using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WaitDemo.Controllers
{
	public class GoodValuesController : ApiController
	{
		// GET api/values
		public async Task<IEnumerable<string>> Get()
		{
			var t = Task.Delay(2000);
			await t;
			return new string[] { "value1", "value2" };
		}
	}
}