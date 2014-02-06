using System;
using NerdDinner.Models;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace NerdDinner.Tests.Models
{
    [TestFixture]
    public class DinnerTest
    {
        [Test]
        public void Dinner_Should_Not_Be_Valid_When_Some_Properties_Incorrect()
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
            Assert.IsFalse(isValid);
        }

        [Test]
        public void Dinner_Should_Be_Valid_When_All_Properties_Correct()
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
