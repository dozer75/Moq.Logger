// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Moq;

namespace KISS.Moq.Logger.Internals
{
    /// <summary>
    ///     Helper methods to handle reflection operations on <see cref="It"/>
    ///     logic.
    /// </summary>
    internal static class ItHelpers
    {
        /// <summary>
        ///     Creates a <see cref="MethodCallExpression"/> for <see cref="It.IsAny{TValue}"/> 
        ///     where TValue is <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of TValue.</param>
        /// <returns>A <see cref="MethodCallExpression"/> for the method.</returns>
        public static MethodCallExpression IsAny(Type type)
        {
            return Expression.Call(Methods.IsAny.MakeGenericMethod(type));
        }

        /// <summary>
        ///     Creates a <see cref="MethodCallExpression"/> for 
        ///     <see cref="It.Is{TValue}(Expression{Func{TValue, bool}})"/> where TValue is 
        ///     <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of TValue.</param>
        /// <returns>A <see cref="MethodCallExpression"/> for the method.</returns>
        public static MethodCallExpression Is(Type type, Expression matcher)
        {
            return Expression.Call(null, Methods.ItIs.MakeGenericMethod(type), matcher);
        }

        private static class Methods
        {
            public static readonly MethodInfo IsAny = typeof(It).GetMethod(nameof(It.IsAny))!;

            public static readonly MethodInfo ItIs = typeof(It).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                               .Single(m => m.Name == nameof(It.Is) &&
                                                                            m.GetParameters().All(p => p.ParameterType.GetGenericArguments()
                                                                                                        .All(ga => ga.IsGenericType &&
                                                                                                                   ga.GetGenericTypeDefinition() == typeof(Func<,>))));
        }
    }
}
