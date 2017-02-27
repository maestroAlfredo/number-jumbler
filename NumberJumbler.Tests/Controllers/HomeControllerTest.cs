using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberJumbler;
using NumberJumbler.Controllers;
using NumberJumbler.Services;

namespace NumberJumbler.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            NumberJumblerWebService service = new NumberJumblerWebService();

            // Arrange
            HomeController controller = new HomeController(service);
            

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
