// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see License.txt.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using KISS.Moq.Logger.Reflection;
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
                mock.Verify(BuildBeginScopeExpression(expression), times, failMessage);
            }
            else if (method.Name.StartsWith("Log", StringComparison.Ordinal))
            {
                mock.Verify(BuildLogExpression(expression), times, failMessage);
            }
            else
            {
                throw new NotSupportedException($"The {method.Name} extension method is not supported by {nameof(VerifyExt)}.");
            }
        }

        private static Expression<Action<TLogger>> BuildBeginScopeExpression<TLogger>(Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            return Expression.Lambda<Action<TLogger>>(Expression.Call(expression.Parameters.Single(), Methods.BeginScopeFormattedLogValues,
                                                                      BuildFormattedLogValuesExpression(expression)),
                                                      expression.Parameters);
        }

        private static NewExpression BuildFormattedLogValuesExpression<TLogger>(Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            var methodCallExpression = (MethodCallExpression)expression.Body;

            ReadOnlyCollection<Expression> methodArguments = methodCallExpression.Arguments;

            IEnumerable<Expression> formattedLogArguments = methodArguments.Skip(methodArguments.Count - 2);

            NoItLogicAllowedExpressionVisitor.Visit(methodCallExpression.Method.Name, formattedLogArguments.ToArray());

            NewExpression formattedLogValues = Expression.New(Constructors.FormattedLogValuesConstructorInfo,
                                                              formattedLogArguments);
            return formattedLogValues;
        }

        private static Expression<Action<TLogger>> BuildLogExpression<TLogger>(Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            return Expression.Lambda<Action<TLogger>>(Expression.Call(expression.Parameters.Single(), Methods.LogFormattedLogValues,
                                                                      BuildLogParameterExpressions(expression)),
                                                      expression.Parameters);
        }

        private static Expression[] BuildLogParameterExpressions<TLogger>(Expression<Action<TLogger>> expression)
            where TLogger : class, ILogger
        {
            var methodCallExpression = (MethodCallExpression)expression.Body;

            List<Expression> logParameters = new();

            Expression[] parameters = methodCallExpression.Arguments.Skip(1).ToArray();

            foreach (ParameterInfo parameterInfo in Methods.LogFormattedLogValues.GetParameters())
            {
                if (parameterInfo.ParameterType == typeof(LogLevel))
                {
                    logParameters.Add(GetLogLevelExpressionFromMethodName(methodCallExpression.Method.Name) ?? parameters.First());
                }
                else if (parameterInfo.ParameterType == typeof(EventId))
                {
                    logParameters.Add(GetExistingParameterExpression(parameters, parameterInfo.ParameterType) ??
                                      Expression.Constant((EventId)0, typeof(EventId)));
                }
                else if (parameterInfo.ParameterType == Types.FormattedLogValues)
                {
                    logParameters.Add(BuildFormattedLogValuesExpression(expression));
                }
                else if (parameterInfo.ParameterType == typeof(Exception))
                {
                    logParameters.Add(GetExistingParameterExpression(parameters, parameterInfo.ParameterType) ??
                                      Expression.Constant(null, typeof(Exception)));
                }
                else if (parameterInfo.ParameterType == Types.MessageFormatter)
                {
                    logParameters.Add(CreateItIsAnyMethodCallExpression(Types.MessageFormatter));
                }
            }

            return logParameters.ToArray();
        }

        private static MethodCallExpression CreateItIsAnyMethodCallExpression(Type valueType)
        {
            return Expression.Call(Methods.ItIsAny.MakeGenericMethod(valueType));
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

        /// <summary>
        ///     A <see cref="ExpressionVisitor"/> that throws <see cref="InvalidOperationException"/>
        ///     if any <see cref="MethodCallExpression"/> uses methods in the <see cref="It"/> helper class.
        /// </summary>
        private sealed class NoItLogicAllowedExpressionVisitor : ExpressionVisitor
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
}
