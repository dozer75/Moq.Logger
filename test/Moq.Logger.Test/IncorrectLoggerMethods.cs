// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace KISS.Moq.Logger.Test
{
    /// <summary>
    ///     Used by <see cref="MoqILoggerExtensionsTests"/> for verification
    ///     of methods that is not supported by <see cref="MockILoggerExtensions"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class IncorrectLoggerMethods
    {
        public static void Foo(this ILogger logger)
        {
            throw new NotSupportedException("Calling this method should fail in before it is executed, review usage.");
        }

        public static void LogTrivia(this ILogger logger, string state)
        {
            throw new NotSupportedException("Calling this method should fail in before it is executed, review usage.");
        }
    }
}
