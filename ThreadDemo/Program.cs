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
			GetIntWithMeasure("Simple native call", x => x.GetSomeInt());
			GetIntWithMeasure("Create empty thread and wait for it", x => x.GetSomeIntWithThread());
			GetIntWithMeasure("Create empty managed thread", x => x.GetSomeIntWithThreadThatDidntStart());
		}

		private static void GetIntWithMeasure(string message, Func<BirthAndDeathLag, int> f)
		{
			Console.WriteLine(message);
			Console.ReadKey();
			Console.WriteLine();
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

			Console.WriteLine("{1:HH.mm.ss}. Some int = {0}", result, DateTime.Now);
			Console.WriteLine("{1:HH.mm.ss}. {0} ms.", s.ElapsedMilliseconds, DateTime.Now);
		}
	}
}
