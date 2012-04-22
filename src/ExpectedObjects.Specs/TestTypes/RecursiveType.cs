namespace ExpectedObjects.Specs.TestTypes
{
	class RecursiveType
	{
		RecursiveType _type;

		public RecursiveType(int value)
		{
			Value = value;
		}

		public int Value { get; set; }

		public RecursiveType Type
		{
			get
			{
				Access++;
				return _type;
			}
			set { _type = value; }
		}

		public decimal Access { get; private set; }
	}
}