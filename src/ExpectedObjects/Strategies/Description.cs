namespace ExpectedObjects.Strategies
{
    public class Description
    {
        public string Label { get; }
        public object Value { get; }
        

        public Description(string label, object value)
        {
            Label = label;
            Value = value;
        }
    }
}