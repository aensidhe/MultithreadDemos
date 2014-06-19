using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public class BirthAndDeathLag
    {

		[DllImport("NativeDll.dll")]
		private static extern int fnNativeDll();

		public int GetSomeInt()
		{
			return fnNativeDll();
		}

		public int GetSomeIntWithThread()
		{
			var t = new Thread(_ => { Console.WriteLine("{1:HH.mm.ss}. ManagedThreadId = {0}", Thread.CurrentThread.ManagedThreadId, DateTime.Now); });
			t.Start();
			t.Join();
			return fnNativeDll();
		}

		public int GetSomeIntWithThreadThatDidntStart()
		{
			var t = new Thread(_ => { Console.WriteLine("{1:HH.mm.ss}. ManagedThreadId = {0}", Thread.CurrentThread.ManagedThreadId, DateTime.Now); });
			return fnNativeDll();
		}
    }
}
