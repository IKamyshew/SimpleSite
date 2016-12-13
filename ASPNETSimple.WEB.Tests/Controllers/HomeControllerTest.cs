using System.Collections.Generic;
using System.Web.Mvc;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.WEB.Controllers;
using NUnit.Framework;

namespace ASPNETSimple.WEB.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Home_GetView_IsModelIsUsersList()
        {
            /*
            //arrange
            var controller = new HomeController();

            //act
            var result = controller.Home() as ViewResult;

            //assert
            Assert.IsInstanceOf<IEnumerable<User>>(result.Model);
            */
        }
    }
}
