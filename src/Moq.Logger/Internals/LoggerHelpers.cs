// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;

namespace KISS.Moq.Logger.Internals
{
    /// <summary>
    ///     Helper methods to handle reflection for the <see cref="ILogger"/>
    ///     methods.
    /// </summary>
    internal static class LoggerHelpers
    {
        /// <summary>
        ///     Gets a <see cref="MethodCallExpression"/> for <see cref="ILogger.BeginScope{TState}(TState)"/>
        ///     where TState is <paramref name="type"/>.
        /// </summary>
        /// <param name="mock">
        ///     A <see cref="Expression"/> representing the <see cref="Mock{T}"/> where T is <see cref="ILogger"/>.
        /// </param>
        /// <param name="type">The <see cref="Type"/> of TState.</param>
        /// <param name="state">A <see cref="Expression"/>representing the value of the state parameter.</param>
        /// <returns>
        ///     A <see cref="MethodCallExpression"/> representing the <see cref="ILogger.BeginScope{TState}(TState)"/>
        ///     operation.
        /// </returns>
        public static MethodCallExpression BeginScope(Expression mock, Type type, Expression state)
        {
            return Expression.Call(mock, Methods.BeginScope.MakeGenericMethod(type), state);
        }

        /// <summary>
        ///     Gets a <see cref="MethodCallExpression"/> for 
        ///     <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/>
        ///     where TState is of type <paramref name="type"/>.
        /// </summary>
        /// <param name="mock">
        ///     A <see cref="Expression"/> representing the <see cref="Mock{T}"/> where T is <see cref="ILogger"/>.
        /// </param>
        /// <param name="type">The <see cref="Type"/> of TState.</param>
        /// <param name="parameters">An array of <see cref="Expression"/> representing the parameters to use.</param>
        /// <returns>
        ///     A <see cref="MethodCallExpression"/> representing the 
        ///     <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/> operation.
        /// </returns>
        public static MethodCallExpression Log(Expression mock, Type type, params Expression[] parameters)
        {
            return Expression.Call(mock, Methods.Log.MakeGenericMethod(type), parameters);
        }

        private static class Methods
        {
            public static readonly MethodInfo BeginScope = typeof(ILogger).GetMethod(nameof(ILogger.BeginScope))!;

            public static readonly MethodInfo Log = typeof(ILogger).GetMethod(nameof(ILogger.Log))!;
        }
    }
}
