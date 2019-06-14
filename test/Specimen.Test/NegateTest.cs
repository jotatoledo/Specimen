// <copyright file="NegateTest.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen.Test
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class NegateTest
    {
        [Theory]
        [InlineData(50, false)]
        [InlineData(20, false)]
        [InlineData(0, true)]
        [InlineData(-20, true)]
        [InlineData(-50, true)]
        public void SatisfiedByYieldCorrectResult(int value, bool expected)
        {
            var isPositive = new TestSpecification<int>(val => val > 0);

            var result = new Negated<int>(isPositive).IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("test", true)]
        [InlineData(null, false)]
        public void SatisfiedByHandlesNull(string value, bool expected)
        {
            var isNull = new TestSpecification<string>(val => val == null);

            var result = new Negated<string>(isNull).IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Fact]
        public void CtorThrowsIfGivenNull()
        {
            Action nullCtor = () => new Negated<int>(null);

            nullCtor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("*[NullGuard]*");
        }
    }
}
