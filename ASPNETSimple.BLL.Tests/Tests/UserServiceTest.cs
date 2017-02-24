using ASPNETSimple.DAL.Interfaces;
using NUnit.Framework;
using Moq;
using ASPNETSimple.BLL.Services.Interfaces;
using ASPNETSimple.BLL.Services;
using System.Collections.Generic;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.BLL.Infrastructure;
using System.Linq;
using AutoMapper;

namespace ASPNETSimple.BLL.Tests.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        IUserService userService;
        public UserServiceTest()
        {
            ASPNETSimple.BLL.Tests.Infrastructure.AutoMapperConfiguration.Configure();
            unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        private List<User> GenerateUsers(int quantity = 0)
        {
            List<User> users = new List<User>();

            for(int i = 0; i < quantity; i++)
            {
                users.Add(new User()
                {
                    Id = i,
                    Email = $"Test{i}@test.test",
                    Password = $"TestPassword{i}",
                    FirstName = $"TestFirstName{i}",
                    LastName = $"TestLastName{i}"
                });
            }

            return users;
        }

        [Test]
        public void GetAllUsers_NoUsersInDB_SuccessNull()
        {
            //arrange
            unitOfWorkMock.Setup(uow => uow.Users.GetAll()).Returns(GenerateUsers() as IEnumerable<User>);
            IEnumerable <UserModel> users;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetAllUsers(out users);

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(users);
        }

        [Test]
        public void GetAllUsers_TwoUsersInDB_SameUsers()
        {
            //arrange
            int usersQuantity = 2;
            List<User> generatedUsers = GenerateUsers(usersQuantity);
            unitOfWorkMock.Setup(uow => uow.Users.GetAll()).Returns(generatedUsers as IEnumerable<User>);
            IEnumerable<UserModel> users;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetAllUsers(out users);
            
            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.AreEqual(usersQuantity, users.Count());

            List<UserModel> usersList = users.ToList();
            
            for (int i = 0; i < usersQuantity; i++)
            {
                Assert.IsTrue(usersList.Any(user => user.Id == generatedUsers[i].Id));
            }
        }

        [Test]
        public void GetUser_ExistingID_UsersWithThisID()
        {
            //arrange
            int id = 3;
            User generatedUser = GenerateUsers(id+1).FirstOrDefault(newUser => newUser.Id == id);
            unitOfWorkMock.Setup(uow => uow.Users.Get(id)).Returns(generatedUser);
            UserModel user;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetUser(id, out user);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void GetUser_NotExistingID_SuccessNull()
        {
            //arrange
            int id = 3;
            User expectedResult = null;
            unitOfWorkMock.Setup(uow => uow.Users.Get(id)).Returns(expectedResult);
            UserModel user;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetUser(id, out user);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.IsNull(user);
        }

        [Test]
        public void GetUser_ExistingLoginPass_SuccessAndExpectedUser()
        {
            //arrange
            User expectedUser = GenerateUsers(1).First();
            unitOfWorkMock.Setup(uow => uow.Users.Get(expectedUser.Email, expectedUser.Password)).Returns(expectedUser);
            UserModel user;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetUser(expectedUser.Email, expectedUser.Password, out user);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.AreEqual(expectedUser.Email, user.Email);
            Assert.AreEqual(expectedUser.Password, user.Password);
        }

        [Test]
        public void GetUser_NotExistingLoginPass_SuccessAndNull()
        {
            //arrange
            User expectedUser = null;
            unitOfWorkMock.Setup(uow => uow.Users.Get("fakeLogin", "fakePass")).Returns(expectedUser);
            UserModel user;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetUser("fakeLogin", "fakePass", out user);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.IsNull(user);
        }

        [Test]
        public void GetUser_EmptyLoginPass_SuccessAndNull()
        {
            //arrange
            User expectedUser = null;
            unitOfWorkMock.Setup(uow => uow.Users.Get("", "")).Returns(expectedUser);
            UserModel user;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.GetUser("", "", out user);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.IsNull(user);
        }

        [Test]
        public void CreateUser_EmptyUser_Failed()
        {
            //arrange
            UserModel newUser = null;

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.CreateUser(newUser);

            //assert
            Assert.IsFalse(result.Succeeded, result.Errors.FirstOrDefault());
            Assert.AreEqual("Cannot create null user.", result.Errors.First());
        }

        [Test]
        public void CreateUser_newUser_Success()
        {
            //arrange
            UserModel newUser = new UserModel()
                                                {
                                                    Id = 1,
                                                    Email = "test1@test.test",
                                                    Password = "TestPassword",
                                                    FirstName = "TestName",
                                                    LastName = "TestLastName"
                                                };
            User expectedEntity = Mapper.Map<UserModel, User>(newUser);

            unitOfWorkMock.Setup(uow => uow.Users.Create(expectedEntity));
            unitOfWorkMock.Setup(uow => uow.Save());

            //act
            userService = new UserService(unitOfWorkMock.Object);
            ServiceResult result = userService.CreateUser(newUser);

            //assert
            Assert.IsTrue(result.Succeeded, result.Errors.FirstOrDefault());
            unitOfWorkMock.Verify(uow => uow.Users.Create(It.IsAny<User>()));
            unitOfWorkMock.Verify(uow => uow.Save());
        }
    }
}
