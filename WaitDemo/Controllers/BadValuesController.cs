using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WaitDemo.Controllers
{
	public class BadValuesController : ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			Thread.Sleep(2000);
			return new string[] { "value1", "value2" };
		}
	}
}
