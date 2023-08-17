// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;

namespace KISS.Moq.Logger.Internals
{
    /// <summary>
    ///     Helper methods to handle reflection on the internal FormattedLogValues type.
    /// </summary>
    internal static class FormattedLogValuesHelpers
    {
        private static readonly MethodCallExpression MessageFormatter = ItHelpers.IsAny(Types.MessageFormatter);

        /// <summary>
        ///     Creates a <see cref="NewExpression"/> that creates a new FormattedLogValues instance
        ///     based on the supplied parameters.
        /// </summary>
        /// <param name="format">A <see cref="Expression"/> representing the format to be used.</param>
        /// <param name="parameters">A <see cref="Expression"/> representing the parameters to be used.</param>
        /// <returns>A <see cref="NewExpression"/> for the FormattedLogValues constructor.</returns>
        public static NewExpression Create(Expression format, Expression parameters)
        {
            return Expression.New(Constructors.Default, format, parameters);
        }

        /// <summary>
        ///     Creates a <see cref="Expression{TDelegate}"/> that represents a call to <see cref="ILogger.BeginScope{TState}(TState)"/>.
        /// </summary>
        /// <typeparam name="TLogger">The type of <see cref="ILogger"/> or <see cref="ILogger{TCategoryName}"/>.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to use.</param>
        /// <param name="state">A <see cref="Expression"/> representing what state to match.</param>
        /// <returns></returns>
        public static Expression<Action<TLogger>> BeginScopeMatch<TLogger>(ParameterExpression mock, Expression state)
            where TLogger : class, ILogger
        {
            return Expression.Lambda<Action<TLogger>>(LoggerHelpers.BeginScope(mock, Types.FormattedLogValues, Match(ToString(state))),
                                                mock);
        }

        /// <summary>
        ///     <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/>
        /// </summary>
        /// <typeparam name="TLogger"></typeparam>
        /// <param name="mock"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Expression<Action<TLogger>> LogMatch<TLogger>(ParameterExpression mock, Expression logLevel, Expression? eventId,
                                                                    Expression state, Expression? exception)
            where TLogger : class, ILogger
        {
            MethodCallExpression match = Match(ToString(state));

            return Expression.Lambda<Action<TLogger>>(LoggerHelpers.Log(mock, Types.FormattedLogValues, logLevel,
                                                                        eventId ?? Expression.Constant((EventId)0, typeof(EventId)),
                                                                        match,
                                                                        exception ?? Expression.Constant(null, typeof(Exception)),
                                                                        MessageFormatter),
                                                      mock);
        }

        private static MethodCallExpression Match(Expression match)
        {
            ParameterExpression objectParameter = Expression.Parameter(Types.FormattedLogValues);

            LambdaExpression matcher = Expression.Lambda(Expression.Equal(match, ToString(objectParameter)),
                                                         objectParameter);

            return ItHelpers.Is(Types.FormattedLogValues, matcher);
        }

        private static MethodCallExpression ToString(Expression expression)
        {
            return Expression.Call(expression, Methods.ToStringMethod);
        }

        private static class Constructors
        {
            public static readonly ConstructorInfo Default = Types.FormattedLogValues.GetConstructor(new Type[] { typeof(string), typeof(object?[]) })!;
        }

        private static class Methods
        {
            public static readonly MethodInfo ToStringMethod = Types.FormattedLogValues.GetMethod(nameof(ToString))!;
        }

        private static class Types
        {
            public static readonly Type FormattedLogValues = typeof(LoggerExtensions).Assembly.GetTypes().Single(t => t.Name == "FormattedLogValues");
            public static readonly Type MessageFormatter = typeof(Func<,,>).MakeGenericType(FormattedLogValues, typeof(Exception), typeof(string));
        }
    }
}
