﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheLines
{
	class TestStruct32Sequential
	{
		private const int Iterations = 100 * 1000 * 1000;
		private int Operations = 2;
		private long StartTime;

		[StructLayout(LayoutKind.Explicit)]
		private struct Data
		{
			[FieldOffset(0)]
			private int LeftOffset;
			
			[FieldOffset(32)]
			public int First;

			[FieldOffset(64)]
			public int Second;
		}

		public CountdownEvent Test()
		{
			var d = new Data();
			StartTime = Stopwatch.GetTimestamp();
			var e = new CountdownEvent(2);

			ThreadPool.QueueUserWorkItem(_ => AccessData(d, 0, e));
			ThreadPool.QueueUserWorkItem(_ => AccessData(d, 1, e));

			return e;
		}

		private void AccessData(Data d, int index, CountdownEvent e)
		{
			for (var i = 0; i < Iterations; i++)
				if (index == 0)
					d.First++;
				else
					d.Second++;

			if (Interlocked.Decrement(ref Operations) != 0)
				Console.WriteLine("Time: {0:N0}", Stopwatch.GetTimestamp() - StartTime);
			e.Signal();
		}
	}
}
