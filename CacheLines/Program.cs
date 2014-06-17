using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wintellect.Threading.LogicalProcessor;

namespace CacheLines
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(17);
			//Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(3);

			TestAll();
			TestAll();
			TestAll();

			Console.ReadKey();
		}

		private static void TestAll()
		{
			Console.WriteLine("Struct auto");
			new TestStruct32().Test().Wait();

			Console.WriteLine("Struct sequential");
			new TestStruct32Sequential().Test().Wait();

			Console.WriteLine("Struct explicit");
			new TestStruct32Explicit().Test().Wait();

			Console.WriteLine("Struct explicit 64");
			new TestStruct64().Test().Wait();

			Console.WriteLine("Class");
			new TestClass().Test().Wait();

			Console.WriteLine("Class explicit");
			new TestClassExplicit().Test().Wait();
		}

		
	}
}
