using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class FixedGenValTest
	{
		private IDataGenerator generator;
		private DataContext context;

		[TestInitialize]
		public void Initialize()
		{
			context = new DataContext();
			generator = new FixedValuesGenerator();
			generator.GenarateData(context);
		}

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
