namespace ExpectedObjects.Reporting
{
    public class NullWriter : IWriter
    {
        public static IWriter Instance = new NullWriter();


        public void Write(EqualityResult content)
        {
        }

        public string GetFormattedResults()
        {
            return string.Empty;
        }
    }
}