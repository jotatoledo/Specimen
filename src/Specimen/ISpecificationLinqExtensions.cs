// <copyright file="ISpecificationLinqExtensions.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Companion class for <see cref="ISpecification{T}"/> which provides support for LINQ expressions.
    /// </summary>
    public static class ISpecificationLinqExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on an specification.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to filter.</param>
        /// <param name="spec">The specification to apply on the sequence.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfied the specification.</returns>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, ISpecification<T> spec) => source.Where(spec.IsSatisfiedBy);

        /// <summary>
        /// Filters a sequence of values based on an specification.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to filter.</param>
        /// <param name="spec">The specification to apply on the sequence.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfied the specification.</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, ISpecification<T> spec) => source.Where(spec.Predicate);
    }
}
