// <copyright file="ISpecificationExtensions.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen
{
    using System;
    using NullGuard;

    /// <summary>
    /// Provides default implementations of logical operations to <see cref="ISpecification{T}"/>.
    /// </summary>
    /// <remarks>
    /// Should be removed once C#@8 lands and interfaces allow default implementations.
    /// </remarks>
    public static class ISpecificationExtensions
    {
        /// <summary>
        /// Conjunction of predicates.
        /// </summary>
        /// <typeparam name="T">The type of values in the domain of the predicates.</typeparam>
        /// <param name="source">The first operand.</param>
        /// <param name="others">The other operands.</param>
        /// <returns>A new specification that represents the conjunction of all others.</returns>
        public static ISpecification<T> And<T>(
            this ISpecification<T> source,
            params ISpecification<T>[] others) => new And<T>(PrePend(source, others));

        /// <summary>
        /// Disjunction of predicates.
        /// </summary>
        /// <typeparam name="T">The type of values in the domain of the predicates.</typeparam>
        /// <param name="source">The first operand.</param>
        /// <param name="others">The other operands.</param>
        /// <returns>A new specification that represents the disjunction of all others.</returns>
        public static ISpecification<T> Or<T>(
            this ISpecification<T> source,
            params ISpecification<T>[] others) => new Or<T>(PrePend(source, others));

        /// <summary>
        /// Negates a predicate.
        /// </summary>
        /// <typeparam name="T">The type of values in the domain of the predicate.</typeparam>
        /// <param name="source">The predicate.</param>
        /// <returns>A new specification that represents the negation of the source one.</returns>
        public static ISpecification<T> Negate<T>(this ISpecification<T> source) => new Negated<T>(source);

        [return: AllowNull]
        private static ISpecification<T>[] PrePend<T>(ISpecification<T> head, ISpecification<T>[] operands)
        {
            if (operands != null)
            {
                // Based on https://stackoverflow.com/a/11402629/5394220
                var newArray = new ISpecification<T>[operands.Length + 1];
                newArray[0] = head;
                Array.Copy(operands, 0, newArray, 1, operands.Length);
                return newArray;
            }
            else
            {
                return null;
            }
        }
    }
}
