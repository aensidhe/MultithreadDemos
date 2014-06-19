using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CacheLines
{
	internal class Program
	{
		private static readonly Dictionary<TestMode, Tuple<long, long, long>> Results = new Dictionary<TestMode, Tuple<long, long, long>>
		{
			{ TestMode.Default, Tuple.Create(long.MaxValue, 0L, 0L) },
			{ TestMode.Slow, Tuple.Create(long.MaxValue, 0L, 0L) },
			{ TestMode.Fast, Tuple.Create(long.MaxValue, 0L, 0L) },
			{ TestMode.Struct, Tuple.Create(long.MaxValue, 0L, 0L) },
		};

		private static Type[] Types;

		internal static void Main(string[] args)
		{
			Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(0x9);

			// Warming up, jitting, etc
			Console.WriteLine("Warming up");
			TestAll(false, 0);

			Console.WriteLine("Testing, HT is off. Tests can take up to 1 minute");
			TestAndPrintOutput();

			Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(0x3);
			foreach (var key in Results.Keys.ToArray())
				Results[key] = Tuple.Create(long.MaxValue, 0L, 0L);

			Console.WriteLine("Testing, HT is on. Tests can take up to 1 minute");
			TestAndPrintOutput();
			Console.ReadKey();
		}

		private static void TestAndPrintOutput()
		{
			for (var i = 0; i < 10; i++)
				TestAll(true, i);

			foreach (var pair in Results)
				Console.WriteLine("{0,-7}: Min = {1,9:N0} ticks, Max = {2,9:N0} ticks, Avg = {3,9:N0} ticks", pair.Key, pair.Value.Item1, pair.Value.Item2, pair.Value.Item3);

		}

		private static void TestAll(bool testMode, int count)
		{
			var distinctCheck = new HashSet<TestMode>();
			foreach (var test in GetTests())
			{
				if (!distinctCheck.Add(test.Mode))
					throw new InvalidOperationException("More than 1 test per mode");

				test.Test().Wait();
				StoreResults(test.Mode, test.ElapsedTicks, testMode, count);
			}
		}

		private static ITest[] GetTests()
		{
			return Assembly.GetExecutingAssembly().GetTypes()
				.Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(typeof(ITest)))
				.Select(x => Activator.CreateInstance(x))
				.Cast<ITest>()
				.ToArray();
		}

		private static void StoreResults(TestMode mode, long ticks, bool testMode, int count)
		{
			if (!testMode)
				return;

			var old = Results[mode];
			Results[mode] = Tuple.Create(
				Math.Min(old.Item1, ticks),
				Math.Max(old.Item2, ticks),
				(old.Item3 * count + ticks) / (count + 1)
			);
		}


	}
}
