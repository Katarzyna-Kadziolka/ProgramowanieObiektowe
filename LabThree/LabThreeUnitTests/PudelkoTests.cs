using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace LabThreeUnitTests;

public class PudelkoTests {
	[SetUp]
	public void Setup() {
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
	}

	[Test]
	public void Test1() {
		Assert.Pass();
	}
}