// <copyright file="Or.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the specification that results from disjuncting others.
    /// </summary>
    /// <typeparam name="T">The type of values that the predicate evaluates on.</typeparam>
    internal sealed class Or<T> : SpecificationBase<T>
    {
        private readonly IEnumerable<ISpecification<T>> operands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Or{T}"/> class.
        /// </summary>
        /// <param name="operands">The operands for the conjunction.</param>
        internal Or(params ISpecification<T>[] operands)
        {
            if (operands.Any(spec => spec == default))
            {
                throw new ArgumentException($"{nameof(operands)} contains a null reference");
            }

            this.operands = operands;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> Predicate => this.operands
                    .Select(spec => spec.Predicate)
                    .Aggregate(CreateOrReducer(Expression.Parameter(typeof(T), "obj")));

        private static Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> CreateOrReducer(ParameterExpression parameter)
            => (left, right)
            => Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(left, parameter),
                        Expression.Invoke(right, parameter)),
                    parameter);
    }
}
