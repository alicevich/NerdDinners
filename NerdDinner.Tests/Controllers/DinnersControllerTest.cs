using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using NerdDinner.Controllers;
using NerdDinner.Models;
using Rhino.Mocks;

namespace NerdDinner.Tests.Controllers
{
    [TestFixture]
    public class DinnersControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            this.stubDinnerRepository = MockRepository.GenerateStub<IDinnerRepository>();
        }

        public IDinnerRepository stubDinnerRepository { get; set; }
        
        [Test]
        public void Details_ValidDinnerID_ReturnsDetailsViewForExistingDinnerID()
        {
            //Arrange
            stubDinnerRepository
                .Stub(sdr => sdr.GetDinner(2))
                .Return(new Dinner());
            var controller = new DinnersController(stubDinnerRepository);
            
           // Act
            var result = controller.Details(1) ;
            
           // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Details_InvalidDinnerID_ReturnsNotFoundView() {
            // Arrange
            var controller = new DinnersController(stubDinnerRepository);
            
            // Act
            var result = controller.Details(999) as ViewResult;
            
            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}
