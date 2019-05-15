using System;
using Technical_Task.Core.CQRS.Queries;
using Technical_Task.Core.Logic;
using Technical_Task.Core.Logic.Validators;
using Xunit;

namespace Technical_Task.Test
{
    public class IsInRangeValidatorTest
    {
        [Theory]
        [InlineData(100)]
        [InlineData(90.80d)]
        [InlineData(80)]
        [InlineData(1.5d)]
        [InlineData(0)]
        public void DoubleInRange(double value)
        {
            Assert.True(new IsInRangeValidator(0, 100).IsValid(value));
        }
        [Theory]
        [InlineData(120)]
        [InlineData(190.80d)]
        [InlineData(200)]
        [InlineData(100.1d)]
        [InlineData(-100)]
        public void DoubleOutOfRange(double value)
        {
            Assert.False(new IsInRangeValidator(0,100).IsValid(value));
        }

        [Theory]
        [InlineData(120)]
        [InlineData(190.80d)]
        [InlineData(200)]
        [InlineData(100.1d)]
        [InlineData(-102)]
        [InlineData(-120)]
        [InlineData(-190.80d)]
        [InlineData(-200)]
        [InlineData(-100.1d)]
        public void DoubleOutOfRangeWithNegatives(double value)
        {
            Assert.False(new IsInRangeValidator(-100, 100).IsValid(value));
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-90.80d)]
        [InlineData(-80)]
        [InlineData(-1.5d)]
        [InlineData(0)]
        public void DoubleInRangeWithNegatives(double value)
        {
            Assert.True(new IsInRangeValidator(-100, 100).IsValid(value));
        }
    }
}
