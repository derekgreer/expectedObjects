namespace ExpectedObjects.Specs.Extensions
{
    public static class StringExpectedObjectExtensions
    {
        public static void ShouldEqual(this string actualString, string expectedString)
        {
            expectedString.ToExpectedObject().ShouldEqual(actualString);
        }
    }
}