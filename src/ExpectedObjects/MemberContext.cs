using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpectedObjects
{
    public class MemberContext<TExpected, TMember> : IMemberContext
    {
        readonly IMemberConfigurationContext _memberConfigurationContext;
        readonly Type _rootType;
        readonly Expression<Func<TExpected, TMember>> _memberExpression;

        public MemberContext(IMemberConfigurationContext memberConfigurationContext, Type rootType,  Expression<Func<TExpected, TMember>> memberExpression)
        {
            _memberConfigurationContext = memberConfigurationContext;
            _rootType = rootType;
            _memberExpression = memberExpression;
        }

        public void UsesComparison(IComparison comparison)
        {
            var memberPath = GetMemberPath(_memberExpression);

            if (_rootType.IsAnonymousType())
            {
                _memberConfigurationContext.ConfigureMember(new RelativeMemberStrategy(comparison, memberPath));
            }
            else
            {
                memberPath = String.Join(".", new [] {_rootType.Name, memberPath}.Where(x => !string.IsNullOrEmpty(x)));
                _memberConfigurationContext.ConfigureMember(new AbsoluteMemberStrategy(comparison, memberPath));
            }
        }

        string GetMemberPath<TSource, TMember>(Expression<Func<TSource, TMember>> expr)
        {
            var members = new Stack<string>();
            MemberExpression memberExpression;

            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    memberExpression = ue?.Operand as MemberExpression;
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