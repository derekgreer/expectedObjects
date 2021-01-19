namespace ExpectedObjects.Chain
{
    class LinkComparisonContext
    {
        public object Expected { get; set; }
        public object Actual { get; set; }
        public string MemberPath { get; set; }
        public bool IgnoreTypeInformation { get; set; }
        public IConfiguration Configuration { get; set; }
        public IComparisonContext ComparisonContext { get; set; }
    }
}