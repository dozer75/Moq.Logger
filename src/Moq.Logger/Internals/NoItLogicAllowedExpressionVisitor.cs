// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Moq;

namespace KISS.Moq.Logger.Internals
{
    /// <summary>
    ///     A <see cref="ExpressionVisitor"/> that throws <see cref="InvalidOperationException"/>
    ///     if any <see cref="MethodCallExpression"/> uses methods in the <see cref="It"/> helper class.
    /// </summary>
    internal sealed class NoItLogicAllowedExpressionVisitor : ExpressionVisitor
    {
        private readonly string _methodName;

        /// <summary>
        ///     Traverses trough the specified <paramref name="expressions"/> to verify
        ///     that they are not called with <see cref="It"/> methods.
        /// </summary>
        /// <param name="methodName">The name of the method initiating this method.</param>
        /// <param name="expressions">The <see cref="Expression"/> instances to check.</param>
        public static void Visit(string methodName, params Expression[] expressions)
        {
            new NoItLogicAllowedExpressionVisitor(methodName).Visit(new ReadOnlyCollection<Expression>(expressions));
        }

        private NoItLogicAllowedExpressionVisitor(string methodName)
        {
            _methodName = methodName;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(It))
            {
                throw new InvalidOperationException($"{nameof(Moq)} {nameof(It)} methods is not supported while verifying {_methodName}.");
            }

            return base.VisitMethodCall(node);
        }
    }
}
