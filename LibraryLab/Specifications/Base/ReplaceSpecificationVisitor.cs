using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace LibraryLab.Specifications.Base
{
    public class ReplaceSpecificationVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;
        public ReplaceSpecificationVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }
}
