using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo
{
	internal static class Program
	{
		private static void Main()
		{
			GetIntWithMeasure(x => x.GetSomeInt());
			GetIntWithMeasure(x => x.GetSomeIntWithThread());
			GetIntWithMeasure(x => x.GetSomeIntWithThreadThatDidntStart());
		}

		private static void GetIntWithMeasure(Func<BirthAndDeathLag, int> f)
		{
			var s = new Stopwatch();
			s.Start();
			int result;
			try
			{
				var a = new BirthAndDeathLag();
				result = f(a);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				result = 0;
			}
			finally
			{
				s.Stop();
			}

			Console.WriteLine("Some int = {0}", result);
			Console.WriteLine("{0} ms. Press any key to continue", s.ElapsedMilliseconds);
			Console.ReadKey();
		}
	}
}
