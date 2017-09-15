namespace ExpectedObjects.Strategies
{
    public class Description
    {
        public Description(string label, object value)
        {
            Label = label;
            Value = value;
        }

        public string Label { get; }
        public object Value { get; }
    }
}