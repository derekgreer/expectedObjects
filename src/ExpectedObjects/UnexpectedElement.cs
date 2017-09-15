namespace ExpectedObjects
{
    public class UnexpectedElement : IUnexpectedElement
    {
        public UnexpectedElement(object element)
        {
            Element = element;
        }

        public object Element { get; set; }
    }
}