namespace ExpectedObjects
{
    public class UnexpectedElement : IUnexpectedElement
    {
        public object Element { get; set; }

        public UnexpectedElement(object element)
        {
            Element = element;
        }
    }
}