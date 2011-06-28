namespace ExpectedObjects
{
    public interface IWriter
    {
        void Write(EqualityResult content);
        string GetFormattedResults();
    }
}