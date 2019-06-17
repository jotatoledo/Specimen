// <copyright file="OrTest.cs" company="Specimen">
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

    public class OrTest
    {
        [Theory]
        [InlineData(50, true)]
        [InlineData(20, true)]
        [InlineData(10, true)]
        [InlineData(0, false)]
        [InlineData(-5, false)]
        public void SatisfiedByYieldCorrectResult(int value, bool expected)
        {
            var isPositive = new TestSpecification<int>(val => val > 0);
            var largerThanTen = new TestSpecification<int>(val => val > 10);
            var largerThanFifthTen = new TestSpecification<int>(val => val > 15);

            var result = new Or<int>(isPositive, largerThanTen, largerThanFifthTen).IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("test", false)]
        [InlineData(null, true)]
        public void SatisfiedByHandlesNull(string value, bool expected)
        {
            var isNull = new TestSpecification<string>(val => val == null);
            var isEmpty = new TestSpecification<string>(val => val != null && val.Length == 0);

            var result = new Or<string>(isNull, isEmpty).IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Fact]
        public void CtorThrowsIfAnyOperandIsNull()
        {
            var isPositive = new TestSpecification<int>(val => val > 0);
            var largerThanTen = new TestSpecification<int>(val => val > 10);

            Action nullOperand = () => new Or<int>(largerThanTen, null);

            nullOperand.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void CtorThrowsIfGivenNull()
        {
            Action nullCtor = () => new Or<int>(null);

            nullCtor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("*[NullGuard]*");
        }
    }
}
