using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheLines
{
	class TestStruct : ITest
	{
		private const int Iterations = 100 * 1000 * 1000;
		private int Operations = 2;
		private long StartTime;

		private struct Data
		{
			public int First;

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

		public long ElapsedTicks { get; private set; }

		private void AccessData(Data d, int index, CountdownEvent e)
		{
			for (var i = 0; i < Iterations; i++)
				if (index == 0)
					d.First++;
				else
					d.Second++;

			if (Interlocked.Decrement(ref Operations) != 0)
				ElapsedTicks = Stopwatch.GetTimestamp() - StartTime;
			e.Signal();
		}


		public TestMode Mode
		{
			get { return TestMode.Struct; }
		}
	}
}
