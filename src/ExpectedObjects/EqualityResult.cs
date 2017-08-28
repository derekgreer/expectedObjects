namespace ExpectedObjects
{
    public enum EqualityResultType
    {
        Comparision,
        Custom
    }

    public class EqualityResult
    {
        public EqualityResult(bool status, string member, object expected, object actual, EqualityResultType resultType = EqualityResultType.Comparision, string message = null)
        {
            Status = status;
            Member = member;
            Expected = expected;
            Actual = actual;
            ResultType = resultType;
            Message = message;
        }

        public bool Status { get; }
        public string Member { get; set; }
        public object Expected { get; }
        public object Actual { get; }
        public EqualityResultType ResultType { get; }
        public string Message { get; }
    }
}