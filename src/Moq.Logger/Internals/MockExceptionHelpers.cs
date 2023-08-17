// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Moq;

using Match = System.Text.RegularExpressions.Match;

namespace KISS.Moq.Logger.Internals
{
    /// <summary>
    ///     Helper methods to handle reflection operations on <see cref="MockException"/>.
    /// </summary>
    internal static class MockExceptionHelpers
    {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
        private static readonly Regex ExtractCallCount = new(", but was (?<CallCount>\\d+) times", RegexOptions.Compiled);
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

        /// <summary>
        ///     Creates a new <see cref="MockException"/> based on the supplied parameters.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Mock{T}"/>.</typeparam>
        /// <param name="oldException">The original thrown <see cref="MockException"/>.</param>
        /// <param name="mock">The <see cref="Mock{T}"/> that caused the exception.</param>
        /// <param name="expression">The expression that caused the exception.</param>
        /// <param name="times">The <see cref="Times"/> rule that caused the exception.</param>
        /// <param name="failMessage">Additional error message to be displayed.</param>
        /// <returns>A <see cref="MockException"/> filled with information based on the supplied parameters.</returns>
        public static MockException CreateNewException<T>(MockException oldException, Mock<T> mock, Expression<Action<T>> expression, Times times, string failMessage)
            where T : class
        {
            Match callCountMatch = ExtractCallCount.Match(oldException.Message);

            int callCount = callCountMatch.Success
                ? int.Parse(callCountMatch.Groups["CallCount"].Value, CultureInfo.InvariantCulture)
                : 0;

            return (MockException)Methods.NoMatchingCalls.Invoke(null, new object[] { mock, expression, failMessage, times, callCount })!;
        }

        /// <summary>
        ///     Helper method to check if the <paramref name="exception"/> was created by no matching calls.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>True if the <paramref name="exception"/> is a no matching calls exception.</returns>
        public static bool IsNoMatchingCalls(MockException exception)
        {
            return (GetReasons(exception) & Reasons.NoMatchingCalls) == Reasons.NoMatchingCalls;
        }

        private static Reasons GetReasons(MockException exception)
        {
            return (Reasons)Properties.Reasons.GetValue(exception, null)!;
        }

        private static class Methods
        {
            public static readonly MethodInfo NoMatchingCalls = typeof(MockException).GetMethod(nameof(NoMatchingCalls), BindingFlags.Static | BindingFlags.NonPublic)!;
        }

        private static class Properties
        {
            public static readonly PropertyInfo Reasons = typeof(MockException).GetProperty(nameof(Reasons), BindingFlags.Instance | BindingFlags.NonPublic)!;
        }

        [Flags]
        private enum Reasons
        {
            IncorrectNumberOfCalls = 1,
            NoMatchingCalls = 4,
            NoSetup = 8,
            ReturnValueRequired = 16,
            UnmatchedSetup = 32,
            UnverifiedInvocations = 64,
        }
    }
}
