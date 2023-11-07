using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.API.Test
{
    public class PostalCodeValidator
    {
        public bool IsValidPostalCode(string postalCode)
        {
            // Check if the postal code is exactly 4 characters long and consists only of digits
            return !string.IsNullOrEmpty(postalCode) && postalCode.Length == 4 && postalCode.All(char.IsDigit);
        }
    }

    [TestFixture]
    public class PostalCodeValidatorTests
    {
        [Test]
        public void IsValidPostalCode_ValidCode_ReturnsTrue()
        {
            // Arrange
            var validator = new PostalCodeValidator();

            // Act
            bool result = validator.IsValidPostalCode("1234");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidPostalCode_InvalidCode_ReturnsFalse()
        {
            // Arrange
            var validator = new PostalCodeValidator();

            // Act
            bool result = validator.IsValidPostalCode("ABCD");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidPostalCode_NullCode_ReturnsFalse()
        {
            // Arrange
            var validator = new PostalCodeValidator();

            // Act
            bool result = validator.IsValidPostalCode(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
