using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public class ConfigurationContext<TExpected> : IConfigurationContext<TExpected>, IConfiguration, IMemberConfigurationContext
    {
        MemberType _memberType;
        Stack<IComparisonStrategy> _strategies = new Stack<IComparisonStrategy>();

        public ConfigurationContext(TExpected @object)
        {
            Object = @object;
        }

        public ICollection<string> IgnoredPaths { get; } = new List<string>();

        public IEnumerable<IComparisonStrategy> Strategies => _strategies;

        public IDictionary<string, IComparison> MemberStrategies { get; } = new Dictionary<string, IComparison>();

        public BindingFlags GetFieldBindingFlags()
        {
            BindingFlags flags = 0;

            if ((_memberType & MemberType.PublicFields) == MemberType.PublicFields)
                flags |= BindingFlags.Public | BindingFlags.Instance;

            return flags;
        }

        public object Object { get; }

        public void PushStrategy<T>() where T : IComparisonStrategy, new()
        {
            _strategies.Push(new T());
        }

        public void PushStrategy(IComparisonStrategy comparisonStrategy)
        {
            _strategies.Push(comparisonStrategy);
        }

        public void IncludeMemberTypes(MemberType memberType)
        {
            _memberType |= memberType;
        }

        public void ReplaceStrategy<TExistingStrategy, TNewStrategy>() where TExistingStrategy : IComparisonStrategy
            where TNewStrategy : IComparisonStrategy, new()
        {
            var list = _strategies.ToList();
            var existingIndex = list.FindIndex(s => typeof(TExistingStrategy) == s.GetType());

            if (existingIndex == -1)
                throw new Exception($"The strategy {typeof(TExistingStrategy)} was not registered.");

            if (existingIndex >= 0)
            {
                list.RemoveAt(existingIndex);
                list.Insert(existingIndex, new TNewStrategy());
                list.Reverse();
                _strategies = new Stack<IComparisonStrategy>(list);
            }
        }

        public void ClearStrategies()
        {
            _strategies.Clear();
        }

        public IMemberContext Member<TMember>(Expression<Func<TExpected, TMember>> memberExpression)
        {
            var underlyingType = Object.GetType();

            var propertyPath = (string) GetType()
                .GetMethod(nameof(GetMemberPath), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(underlyingType, typeof(TMember))
                .Invoke(this, new object[] {memberExpression});

            var sb = new StringBuilder();
            sb.Append(underlyingType.Name);


            if (!string.IsNullOrWhiteSpace(propertyPath))
                sb.Append($".{propertyPath}");

            return new MemberContext(this, sb.ToString());
        }

        void IMemberConfigurationContext.ConfigureMember(string memberPath, IComparison comparison)
        {
            MemberStrategies.Add(memberPath, comparison);
        }

        string GetMemberPath<TSource, TMember>(Expression<Func<TSource, TMember>> expr)
        {
            var members = new Stack<string>();
            var sb = new StringBuilder();
            MemberExpression memberExpression;

            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    memberExpression = (ue != null ? ue.Operand : null) as MemberExpression;
                    break;
                default:
                    memberExpression = expr.Body as MemberExpression;
                    break;
            }

            while (memberExpression != null)
            {
                if (memberExpression.Expression.NodeType == ExpressionType.Call)
                {
                    var propertyName = memberExpression.Member.Name;

                    var methodCallExpression = memberExpression.Expression as MethodCallExpression;
                    if (methodCallExpression.Method.Name == "get_Item")
                    {
                        members.Push($"{((MemberExpression)methodCallExpression.Object).Member.Name}[{methodCallExpression.Arguments[0]}].{propertyName}");
                    }

                    memberExpression = memberExpression.Expression as MemberExpression; 
                }
                else
                {
                    var propertyName = memberExpression.Member.Name;
                    members.Push(propertyName);
                    memberExpression = memberExpression.Expression as MemberExpression;    
                }

                

                
            }

            return string.Join(".", members.ToArray());
        }
    }
}