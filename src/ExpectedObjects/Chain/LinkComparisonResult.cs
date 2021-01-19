namespace ExpectedObjects.Chain
{
    class LinkComparisonResult
    {
        public static readonly LinkComparisonResult False = new LinkComparisonResult { Result = false};
        public static readonly LinkComparisonResult True = new LinkComparisonResult { Result = true};
        public bool Result { get; set; }
        public object ExpectedResult { get; set; }
    }
}