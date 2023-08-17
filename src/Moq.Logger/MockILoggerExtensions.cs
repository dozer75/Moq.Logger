// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using KISS.Moq.Logger.Internals;
using Microsoft.Extensions.Logging;
using Moq;

namespace KISS.Moq.Logger
{
    /// <summary>
    ///     Extension methods to enable verifying <see cref="ILogger"/> extension methods from
    ///     <see cref="LoggerExtensions"/>.
    /// </summary>
    public static class MockILoggerExtensions
    {
        private static readonly Dictionary<string, Expression?> LogMethodToLogLevel = new()
        {
            { nameof(LoggerExtensions.LogTrace), Expression.Constant(LogLevel.Trace) },
            { nameof(LoggerExtensions.LogDebug), Expression.Constant(LogLevel.Debug) },
            { nameof(LoggerExtensions.LogInformation), Expression.Constant(LogLevel.Information) },
            { nameof(LoggerExtensions.LogWarning), Expression.Constant(LogLevel.Warning) },
            { nameof(LoggerExtensions.LogError), Expression.Constant(LogLevel.Error) },
            { nameof(LoggerExtensions.LogCritical), Expression.Constant(LogLevel.Critical) },
            { nameof(LoggerExtensions.Log), null }
        };

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        [ExcludeFromCodeCoverage(
#if NET6_0_OR_GREATER
            Justification = "Just a wrapper for base functionality"
#endif
        )]
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            mock.VerifyExt(expression, (string)null!);
        }

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        [ExcludeFromCodeCoverage(
#if NET6_0_OR_GREATER
            Justification = "Just a wrapper for base functionality"
#endif
        )]
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression, string failMessage)
            where TLogger : class, ILogger
        {
            mock.VerifyExt(expression, Times.AtLeastOnce(), failMessage!);
        }

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        [ExcludeFromCodeCoverage(
#if NET6_0_OR_GREATER
            Justification = "Just a wrapper for base functionality"
#endif
        )]
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression, Func<Times> times)
            where TLogger : class, ILogger
        {
            mock.VerifyExt(expression, times, null!);
        }

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        [ExcludeFromCodeCoverage(
#if NET6_0_OR_GREATER
            Justification = "Just a wrapper for base functionality"
#endif
        )]
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression, Func<Times> times, string failMessage)
            where TLogger : class, ILogger
        {
            mock.VerifyExt(expression, times?.Invoke() ?? Times.AtLeastOnce(), failMessage!);
        }

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        [ExcludeFromCodeCoverage(
#if NET6_0_OR_GREATER
            Justification = "Just a wrapper for base functionality"
#endif
        )]
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression, Times times)
            where TLogger : class, ILogger
        {
            mock.VerifyExt(expression, times, null!);
        }

        /// <summary>
        ///   Verifies that a specific invocation matching the given expression was performed on the <see cref="ILogger"/> mock.
        ///   Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
        /// </summary>
        /// <typeparam name="TLogger"><see cref="ILogger"/> type to mock.</typeparam>
        /// <param name="mock">The <see cref="Mock{T}"/> to verify.</param>
        /// <param name="expression">Expression to verify.</param>
        /// <param name="times">The number of times a method is expected to be called.</param>
        /// <param name="failMessage">Message to show if verification fails.</param>
        /// <exception cref="MockException">The invocation was not performed on the mock.</exception>
        /// <exception cref="InvalidOperationException">Something failed during setup of the verification.</exception>
        public static void VerifyExt<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression, Times times, string failMessage)
            where TLogger : class, ILogger
        {
            if (mock is null)
            {
                throw new ArgumentNullException(nameof(mock));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MethodInfo method = ((MethodCallExpression)expression.Body).Method;

            if (method.GetCustomAttribute<ExtensionAttribute>() == null)
            {
                throw new NotSupportedException($"Use {nameof(Mock)}.{nameof(Mock.Verify)} methods for non-extension methods.");
            }
            else if (method.Name == nameof(LoggerExtensions.BeginScope))
            {
                mock.VerifyWithExceptionHandling(FormattedLogValuesHelpers.BeginScopeMatch<TLogger>(expression.Parameters.Single(),
                                                                                                    CreateFormattedLogValues(expression)),
                                                 times, failMessage, expression);
            }
            else if (method.Name.StartsWith("Log", StringComparison.Ordinal))
            {
                var methodCallExpression = (MethodCallExpression)expression.Body;

                Expression[] parameters = methodCallExpression.Arguments.Skip(1).ToArray();

                Expression logLevel = GetLogLevelExpressionFromMethodName(methodCallExpression.Method.Name) ?? parameters.First();
                Expression? eventId = GetExistingParameterExpression(parameters, typeof(EventId));
                Expression formattedLogValues = CreateFormattedLogValues(expression);
                Expression? exception = GetExistingParameterExpression(parameters, typeof(Exception));

                mock.VerifyWithExceptionHandling(FormattedLogValuesHelpers.LogMatch<TLogger>(expression.Parameters.Single(), logLevel, eventId,
                                                                                             formattedLogValues, exception),
                                                 times, failMessage, expression);
            }
            else
            {
                throw new NotSupportedException($"The {method.Name} extension method is not supported by {nameof(VerifyExt)}.");
            }
        }

        private static NewExpression CreateFormattedLogValues<TLogger>(Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            var methodCallExpression = (MethodCallExpression)expression.Body;

            ReadOnlyCollection<Expression> methodArguments = methodCallExpression.Arguments;

            Expression[] formattedLogArguments = methodArguments.Skip(methodArguments.Count - 2).ToArray();

            NoItLogicAllowedExpressionVisitor.Visit(methodCallExpression.Method.Name, formattedLogArguments);

            return FormattedLogValuesHelpers.Create(formattedLogArguments.First(), formattedLogArguments.Last());
        }

        private static Expression? GetLogLevelExpressionFromMethodName(string methodName)
        {
            return LogMethodToLogLevel.TryGetValue(methodName, out Expression? expression)
                ? expression
                : throw new NotSupportedException($"The log extension method {methodName} is not supported by {nameof(VerifyExt)}.");
        }

        private static Expression? GetExistingParameterExpression(IEnumerable<Expression> parameters, Type type)
        {
            return parameters.FirstOrDefault(p => p.Type.IsSubclassOf(type));
        }

        private static void VerifyWithExceptionHandling<TLogger>(this Mock<TLogger> mock, Expression<Action<TLogger>> expression,
                                                                 Times times, string failMessage, Expression<Action<TLogger>> originalExpression)
            where TLogger : class, ILogger
        {
            try
            {
                mock.Verify(expression, times, failMessage);
            }
            catch (MockException e)
                when (MockExceptionHelpers.IsNoMatchingCalls(e))
            {
                throw MockExceptionHelpers.CreateNewException(e, mock, originalExpression, times, failMessage);
            }
        }
    }
}
