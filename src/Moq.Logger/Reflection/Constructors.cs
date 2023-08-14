// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see License.txt.

using System;
using System.Reflection;

namespace KISS.Moq.Logger.Reflection
{
    /// <summary>
    ///     Various <see cref="ConstructorInfo"/> retrieved using reflection.
    /// </summary>
    internal static class Constructors
    {
        /// <summary>
        ///     A <see cref="ConstructorInfo"/> used to create an instance of the internal type <see cref="Types.FormattedLogValues"/>.
        /// </summary>
        public static readonly ConstructorInfo FormattedLogValuesConstructorInfo = Types.FormattedLogValues
                                                                                        .GetConstructor(new Type[] { typeof(string), typeof(object?[]) })!;
    }
}
