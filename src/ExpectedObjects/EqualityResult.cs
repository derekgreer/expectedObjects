namespace ExpectedObjects
{
    public class EqualityResult
    {
        public EqualityResult(bool status, string member, object expected, object actual)
        {
            Status = status;
            Member = member;
            Expected = expected;
            Actual = actual;
        }

        public bool Status { get; private set; }
        public string Member { get; set; }
        public object Expected { get; private set; }
        public object Actual { get; private set; }
    }
}