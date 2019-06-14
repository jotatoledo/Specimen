// <copyright file="TestSpecification.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen.Test
{
    using System;
    using System.Linq.Expressions;

    internal class TestSpecification<T> : SpecificationBase<T>
    {
        public TestSpecification(Func<T, bool> predicate)
        {
            // Based on https://stackoverflow.com/a/9377725/5394220
            this.Predicate = val => predicate(val);
        }

        public override Expression<Func<T, bool>> Predicate { get; }
    }
}
