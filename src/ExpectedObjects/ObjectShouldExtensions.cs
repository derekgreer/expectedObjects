namespace ExpectedObjects
{
    public static class ObjectShouldExtensions
    {
        public static void ShouldEqual<T>(this T actual, ExpectedObject expected)
        {
            expected.ShouldEqual(actual);
        }

        public static void ShouldNotEqual<T>(this T actual, ExpectedObject expected)
        {
            expected.ShouldNotEqual(actual);
        }

        public static void ShouldMatch<T>(this T actual, ExpectedObject expected)
        {
            expected.ShouldMatch(actual);
        }

        public static void ShouldNotMatch<T>(this T actual, ExpectedObject expected)
        {
            expected.ShouldNotMatch(actual);
        }
    }
}