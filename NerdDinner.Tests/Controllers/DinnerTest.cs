using System;
using NerdDinner.Models;
using NUnit.Framework;

namespace NerdDinner.Tests.Models
{
    [TestFixture]
    public class DinnerTest
    {
        [Test]
        public void IncorrectDinnerProperties_DinnerIsNotValid()
        {
            //Arrange
            var dinner = new Dinner
                {
                Title = "Test title",
                Country = "USA",
                ContactPhone = "BOGUS"
            };

            // Act
            var isValid = dinner.IsValid;

            //Assert
            Assert.False(isValid);
        }

        [Test]
        public void DinnerPropertiesAreCorrect_DinnerIsValid()
        {
            //Arrange
            var dinner = new Dinner
            {
                Title = "Test title",
                Description = "Some description",
                EventDate = DateTime.Now,
                HostedBy = "ScottGu",
                Address = "One Microsoft Way",
                Country = "USA",
                ContactPhone = "425-703-8072",
                Latitude = 93,
                Longitude = -92,
            };

            // Act
            var isValid = dinner.IsValid;

            //Assert
            Assert.IsTrue(isValid);
        }
    }
}
