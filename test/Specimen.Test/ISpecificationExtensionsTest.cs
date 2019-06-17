// <copyright file="ISpecificationExtensionsTest.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen.Test
{
    using System;
    using FluentAssertions;
    using NSubstitute;
    using Xunit;

    public class ISpecificationExtensionsTest
    {
        [Fact]
        public void AndThrowsIfAnyOperandIsNull()
        {
            var spec = CreateFakeSpec<string>();

            Action nullOPerand = () => spec.And(CreateFakeSpec<string>(), null);

            nullOPerand.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void OrThrowsIfAnyOperandIsNull()
        {
            var spec = CreateFakeSpec<string>();

            Action nullOPerand = () => spec.Or(CreateFakeSpec<string>(), null);

            nullOPerand.Should()
                .Throw<ArgumentException>();
        }

        private static ISpecification<T> CreateFakeSpec<T>()
        {
            var fake = Substitute.For<ISpecification<T>>();
            return fake;
        }
    }
}
