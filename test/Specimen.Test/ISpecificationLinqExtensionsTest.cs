// <copyright file="ISpecificationLinqExtensionsTest.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Specimen.Test
{
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class ISpecificationLinqExtensionsTest
    {
        [Theory]
        [InlineData(new[] { -10, 5, -5, 0, 10 }, new[] { 5, 10 })]
        [InlineData(new[] { 1, 2 }, new[] { 1, 2 })]
        public void ItCorrecltyWorksInLinqToObjects(int[] input, int[] expected)
        {
            var spec = new TestSpecification<int>(val => val > 0);

            var result = input.Where(spec);

            result.Should()
                .BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(new[] { -10, 5, -5, 0, 10 }, new[] { 5, 10 })]
        [InlineData(new[] { 1, 2 }, new[] { 1, 2 })]
        public void ItCorrecltyWorksInLinqToSql(int[] input, int[] expected)
        {
            var spec = new TestSpecification<int>(val => val > 0);

            var result = input.AsQueryable().Where(spec);

            result.Should()
                .BeEquivalentTo(expected);
        }
    }
}
