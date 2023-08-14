// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see License.txt.

using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace KISS.Moq.Logger.Reflection
{
    /// <summary>
    ///     Various <see cref="Type"/> retrieved using reflection.
    /// </summary>
    internal static class Types
    {
        /// <summary>
        ///     The internal FormattedLogValues type in Microsoft.Extensions.Logging.Abstractions <see cref="Type"/>.
        /// </summary>
        /// <remarks>
        ///     Since it is an internal type, this is the only way to get it.
        /// </remarks>
        public static readonly Type FormattedLogValues = typeof(LoggerExtensions).Assembly.GetTypes().Single(t => t.Name == "FormattedLogValues");

        /// <summary>
        ///     A <see cref="Func{T1, T2, TResult}"/> where T1 is the internal type <see cref="FormattedLogValues"/>, T2 is type of 
        ///     <see cref="Exception"/> and TResult is type of <see cref="string"/>.
        /// </summary>
        public static readonly Type MessageFormatter = typeof(Func<,,>).MakeGenericType(FormattedLogValues, typeof(Exception), typeof(string));
    }
}
