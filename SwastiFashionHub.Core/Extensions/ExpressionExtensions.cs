using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);
            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this FilterDescriptor filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.Property);
            var value = Expression.Constant(filter.FilterValue);
            var left = Expression.Call(property, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            var right = Expression.Call(value, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            var body = default(Expression);
            switch (filter.FilterOperator)
            {
                case FilterOperator.Contains:
                    body = Expression.Call(left, typeof(string).GetMethod("Contains"), right);
                    break;
                case FilterOperator.Equals:
                    body = Expression.Equal(left, right);
                    break;
                case FilterOperator.NotEquals:
                    body = Expression.NotEqual(left, right);
                    break;
                case FilterOperator.LessThan:
                    body = Expression.LessThan(left, right);
                    break;
                case FilterOperator.LessThanOrEquals:
                    body = Expression.LessThanOrEqual(left, right);
                    break;
                case FilterOperator.GreaterThan:
                    body = Expression.GreaterThan(left, right);
                    break;
                case FilterOperator.GreaterThanOrEquals:
                    body = Expression.GreaterThanOrEqual(left, right);
                    break;
                case FilterOperator.StartsWith:
                    body = Expression.Call(left, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), right);
                    break;
                case FilterOperator.EndsWith:
                    body = Expression.Call(left, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), right);
                    break;
                case FilterOperator.DoesNotContain:
                    body = Expression.Not(Expression.Call(left, typeof(string).GetMethod("Contains"), right));
                    break;
                case FilterOperator.IsNull:
                    body = Expression.Equal(property, Expression.Constant(null));
                    break;
                case FilterOperator.IsEmpty:
                    body = Expression.Equal(property, Expression.Constant(string.Empty));
                    break;
                case FilterOperator.IsNotNull:
                    body = Expression.NotEqual(property, Expression.Constant(null));
                    break;
                case FilterOperator.IsNotEmpty:
                    body = Expression.Not(Expression.Call(left, typeof(string).GetMethod("Contains"), right));
                    break;
                default:
                    throw new NotSupportedException($"Filter operator '{filter.FilterOperator}' not supported.");
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        //public static Expression<Func<T, bool>> BuildFilter<T>(this IEnumerable<FilterDescriptor> filterDescriptors)
        //{
        //    var filter = FilterDescriptorToExpressionBuilder.Build<T>(filterDescriptors);
        //    return filter.ToExpression<Func<T, bool>>(new ExpressionConverter());
        //}


        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                return node == _oldValue ? _newValue : base.Visit(node);
            }
        }
    }
}
