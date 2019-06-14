// <copyright file="Negated.cs" company="Specimen">
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
    /// Represents the specification that results from negating another one.
    /// </summary>
    /// <typeparam name="T">The type of values that the predicate evaluates on.</typeparam>
    internal sealed class Negated<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="Negated{T}"/> class.
        /// </summary>
        /// <param name="inner">The predicate to be negated.</param>
        internal Negated(ISpecification<T> inner)
        {
            this.inner = inner;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(this.inner.Predicate, objParam)),
                    objParam);

                return newExpr;
            }
        }
    }
}
