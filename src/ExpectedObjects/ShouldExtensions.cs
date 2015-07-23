using System;

namespace ExpectedObjects
{
	public static class ShouldExtensions
	{
		public static void ShouldEqual<T>(this ExpectedObject expected, T actual)
		{
			IWriter writer = new ShouldWriter();
			expected.Configure(ctx => ctx.SetWriter(writer));
			expected.Equals(actual);
			string results = writer.GetFormattedResults();

			if (!string.IsNullOrEmpty(results))
				throw new Exception(results);
		}

		public static void ShouldNotEqual<T>(this ExpectedObject expected, T actual)
		{
			if (expected.Equals(actual))
				throw new Exception(string.Format("For {0}, should not equal expected object but does.{1}",
					actual.ToUsefulString(), Environment.NewLine));
		}

		public static void ShouldMatch<T>(this ExpectedObject expected, T actual)
		{
			var writer = new ShouldWriter();
			expected.Configure(ctx =>
			{
				ctx.SetWriter(writer);
				ctx.IgnoreTypes();
			});
			expected.Equals(actual);
			string results = writer.GetFormattedResults();

			if (!string.IsNullOrEmpty(results))
				throw new Exception(results);
		}

		public static void ShouldNotMatch<T>(this ExpectedObject expected, T actual)
		{
			if (expected.Configure(ctx => ctx.IgnoreTypes()).Equals(actual))
				throw new Exception(string.Format("For {0}, should not equal expected object but does.{1}",
					actual.ToUsefulString(), Environment.NewLine));
		}
	}
}