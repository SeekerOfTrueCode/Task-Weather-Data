using System;
using Technical_Task.Core.CQRS.Queries;
using Technical_Task.Core.Logic;
using Technical_Task.Core.Logic.Validators;
using Xunit;

namespace Technical_Task.Test
{
    public class PercentageValidatorTest
    {
        [Theory]
        [InlineData(100)]
        [InlineData(90.80d)]
        [InlineData(80)]
        [InlineData(1.5d)]
        [InlineData(0)]
        public void DoublePercentageInRange(double value)
        {
            Assert.True(new PercentageValidator().IsValid(value));
        }
        [Theory]
        [InlineData(120)]
        [InlineData(190.80d)]
        [InlineData(200)]
        [InlineData(100.1d)]
        [InlineData(-100)]
        public void DoublePercentageOutOfRange(double value)
        {
            Assert.False(new PercentageValidator().IsValid(value));
        }
    }
}
