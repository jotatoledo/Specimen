// <copyright file="SpecificationBase.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen
{
    using System;
    using System.Linq.Expressions;
    using NullGuard;

    /// <summary>
    /// Base implementation of <see cref="ISpecification{T}"/> that allows caching of a compiled predicate expression.
    /// </summary>
    /// <typeparam name="T">The type of values in the domain of the predicate.</typeparam>
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> compiledExpression;

        /// <inheritdoc/>
        public abstract Expression<Func<T, bool>> Predicate { get; }

        private Func<T, bool> CompiledExpression => this.compiledExpression ?? (this.compiledExpression = this.Predicate.Compile());

        /// <inheritdoc/>
        public bool IsSatisfiedBy([AllowNull] T value) => this.CompiledExpression(value);
    }
}
