using System.Collections.Generic;
using System.Web.Mvc;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.BLL.Services.Interfaces;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.WEB.Controllers;
using Moq;
using NUnit.Framework;

namespace ASPNETSimple.WEB.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        Mock<IUserService> userService;
        public HomeControllerTest()
        {
            userService = new Mock<IUserService>();
        }

        private IEnumerable<UserModel> CreateUserModels(int quantity)
        {
            List<UserModel> usersList = new List<UserModel>();

            for(int i = 0; i < quantity; i++)
            {
                usersList.Add(new UserModel()
                {
                    Id = 123456789,
                    Email = $"TestUser{i}@dot.com",
                    Password = $"password{i}",
                    FirstName = $"test{i}",
                    LastName = $"ltest{i}"
                });
            }

            return usersList as IEnumerable<UserModel>;
        }

        [Test]
        public void Home_GetView_IsModelIsUsersModelsList()
        {
            //arrange
            IEnumerable<UserModel> users = CreateUserModels(1);
            userService.Setup(service => service.GetAllUsers(out users)).Returns(ServiceResult.Success);
            IUserService userServiceObject = userService.Object;
            var controller = new HomeController(userServiceObject);

            //act
            var result = controller.Home() as ViewResult;
            var model = result.Model as List<UserModel>;

            //assert
            Assert.AreEqual(1, model.Count);
            Assert.IsInstanceOf<IEnumerable<UserModel>>(result.Model);
        }

        [Test]
        public void Home_GetView_IsModelNull()
        {
            //arrange
            IEnumerable<UserModel> users = null;
            userService.Setup(service => service.GetAllUsers(out users)).Returns(ServiceResult.Success);
            IUserService userServiceObject = userService.Object;
            var controller = new HomeController(userServiceObject);

            //act
            var result = controller.Home() as ViewResult;

            //assert
            Assert.IsNull(result.Model);
        }
    }
}
