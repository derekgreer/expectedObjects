namespace ExpectedObjects
{
    public interface IConfigurationContext
    {
        void SetWriter(IWriter writer);
        void IgnoreTypes();
        void PushStrategy<T>() where T : IComparisonStrategy, new();
        void PushStrategy(IComparisonStrategy comparisonStrategy);
        void Include(MemberType memberType);
    }
}