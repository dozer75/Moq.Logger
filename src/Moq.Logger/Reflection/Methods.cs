// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see License.txt.

using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;

namespace KISS.Moq.Logger.Reflection
{
    /// <summary>
    ///     Various <see cref="MethodInfo"/> retrieved using reflection.
    /// </summary>
    internal static class Methods
    {
        /// <summary>
        ///     <see cref="ILogger.BeginScope{TState}(TState)"/> <see cref="MethodInfo"/> where TState is the internal type
        ///     <see cref="Types.FormattedLogValues"/>.
        /// </summary>
        public static readonly MethodInfo BeginScopeFormattedLogValues = typeof(ILogger).GetMethod(nameof(ILogger.BeginScope))!
                                                                                        .MakeGenericMethod(Types.FormattedLogValues);

        /// <summary>
        ///     <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/> 
        ///     <see cref="MethodInfo"/> where TState is the internal type <see cref="Types.FormattedLogValues"/>.
        /// </summary>
        public static readonly MethodInfo LogFormattedLogValues = typeof(ILogger).GetMethod(nameof(ILogger.Log))!
                                                                                 .MakeGenericMethod(Types.FormattedLogValues);

        /// <summary>
        ///     <see cref="It.IsAny{TValue}"/> <see cref="MethodInfo"/>.
        /// </summary>
        public static readonly MethodInfo ItIsAny = typeof(It).GetMethod(nameof(It.IsAny))!;
    }
}
