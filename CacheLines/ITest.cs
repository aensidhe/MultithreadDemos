using System;
namespace CacheLines
{
	interface ITest
	{
		long ElapsedTicks { get; }
		TestMode Mode { get; }
		System.Threading.CountdownEvent Test();
	}
}
