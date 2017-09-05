namespace ExpectedObjects
{
    public interface IMemberConfigurationContext
    {
        void ConfigureMember(string memberPath, IComparison comparison);
    }
}