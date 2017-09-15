namespace ExpectedObjects
{
    public class ExpectedDescription : IExpectedDescription
    {
        readonly object _expectedResult;

        public ExpectedDescription(object expectedResult)
        {
            _expectedResult = expectedResult;
        }

        public override string ToString()
        {
            return _expectedResult.ToString();
        }
    }
}