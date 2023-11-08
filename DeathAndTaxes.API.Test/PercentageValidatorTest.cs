using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.API.Test
{
    public class PercentageValidatorTest
    {
        public bool IsPercentage(double value)
        {
            return value >= 0.0 && value <= 100.0;
        }
    }

    [TestFixture]
    public class PercentageValidatorTests
    {
        [Test]
        public void IsPercentage_ShouldReturnTrueForValidPercentage()
        {
            // Arrange
            var validator = new PercentageValidatorTest();

            // Test valid percentage values
            double validPercentage1 = 0.0;
            double validPercentage2 = 50.0;
            double validPercentage3 = 100.0;

            // Act and Assert
            Assert.IsTrue(validator.IsPercentage(validPercentage1));
            Assert.IsTrue(validator.IsPercentage(validPercentage2));
            Assert.IsTrue(validator.IsPercentage(validPercentage3));
        }

        [Test]
        public void IsPercentage_ShouldReturnFalseForInvalidPercentage()
        {
            // Arrange
            var validator = new PercentageValidatorTest();

            // Test invalid percentage values
            double invalidPercentage1 = -10.0;
            double invalidPercentage2 = 150.0;
            double invalidPercentage3 = 42.5;

            // Act and Assert
            Assert.IsFalse(validator.IsPercentage(invalidPercentage1));
            Assert.IsFalse(validator.IsPercentage(invalidPercentage2));
            Assert.IsTrue(validator.IsPercentage(invalidPercentage3));
        }
    }
}
