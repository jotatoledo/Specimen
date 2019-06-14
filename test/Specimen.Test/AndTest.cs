// <copyright file="AndTest.cs" company="Specimen">
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

    public class AndTest
    {
        [Theory]
        [InlineData(50, true)]
        [InlineData(20, true)]
        [InlineData(15, false)]
        [InlineData(10, false)]
        [InlineData(5, false)]
        public void SatisfiedByYieldCorrectResult(int value, bool expected)
        {
            var isPositive = new TestSpecification<int>(val => val > 0);
            var largerThanTen = new TestSpecification<int>(val => val > 10);
            var largerThanFifthTen = new TestSpecification<int>(val => val > 15);

            var result = isPositive
                .And(largerThanTen, largerThanFifthTen)
                .IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("test", true)]
        [InlineData(null, false)]
        public void SatisfiedByHandlesNull(string value, bool expected)
        {
            var notNull = new TestSpecification<string>(val => val != null);
            var notEmpty = new TestSpecification<string>(val => val != null && val.Length != 0);

            var result = new And<string>(notNull, notEmpty).IsSatisfiedBy(value);

            result.Should()
                .Be(expected);
        }

        [Fact]
        public void CtorThrowsIfAnyOperandIsNull()
        {
            var isPositive = new TestSpecification<int>(val => val > 0);
            var largerThanTen = new TestSpecification<int>(val => val > 10);

            Action nullOperand = () => isPositive.And(largerThanTen, null);

            nullOperand.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void CtorThrowsIfGivenNull()
        {
            Action nullCtor = () => new And<int>(null);

            nullCtor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("*[NullGuard]*");
        }
    }
}
