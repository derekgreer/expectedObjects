namespace ExpectedObjects.Reporting
{
    public interface IWriter
    {
        void Write(EqualityResult content);
        string GetFormattedResults();
    }
}