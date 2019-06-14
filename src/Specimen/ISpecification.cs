// <copyright file="ISpecification.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a logical predicate that can be evluated on values of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values in the domain of the predicate.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the predicate function.
        /// </summary>
        Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Determines if the <see cref="Predicate"/> is satified by a value.
        /// </summary>
        /// <param name="value">The value on which <see cref="Predicate"/> is evaluated.</param>
        /// <returns>True if the value meets the predicate; false otherwise.</returns>
        bool IsSatisfiedBy(T value);
    }
}
