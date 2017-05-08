using System.Collections.Generic;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
	public class ConfigurationContext : IConfigurationContext, IConfiguredContext
	{
		readonly Stack<IComparisonStrategy> _strategies = new Stack<IComparisonStrategy>();
		bool _ignoreTypes;
		MemberType _memberType;
		IWriter _writer = NullWriter.Instance;

		public void SetWriter(IWriter writer)
		{
			_writer = writer;
		}

		void IConfigurationContext.IgnoreTypes()
		{
			_ignoreTypes = true;
		}

		public void PushStrategy<T>() where T : IComparisonStrategy, new()
		{
			_strategies.Push(new T());
		}

		public void PushStrategy(IComparisonStrategy comparisonStrategy)
		{
			_strategies.Push(comparisonStrategy);
		}

		public void Include(MemberType memberType)
		{
			_memberType |= memberType;
		}

		public IEnumerable<IComparisonStrategy> Strategies
		{
			get { return _strategies; }
		}

		public IWriter Writer
		{
			get { return _writer; }
		}

		bool IConfiguredContext.IgnoreTypes
		{
			get { return _ignoreTypes; }
		}

		public BindingFlags GetFieldBindingFlags()
		{
			BindingFlags flags = 0;

			if ((_memberType & MemberType.PublicFields) == MemberType.PublicFields)
			{
				flags |= BindingFlags.Public | BindingFlags.Instance;
			}

			return flags;
		}
	}
}