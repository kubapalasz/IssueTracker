using System;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueTrackerApi.Services.Integration.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void UniqueIserIsSaveWithNoError()
        {
            // Arrange
            IUserRepository userRepository = new UserRepository();
            var login = "testLogin@home.pl" + DateTime.Now.ToLongTimeString();
            var user = new User {Login = login};

            // Act
            userRepository.Save(user);

            // Assert
            var loadedUser = userRepository.GetByLogin(login);
            Assert.IsNotNull(loadedUser);
            Assert.AreEqual(loadedUser.Login, login);
        }

        [TestMethod]
        public void SaveExistingUserOnlyUpdatesDetailsDoesNotIncreaseUsersAmount()
        {
            // Arrange
            IUserRepository userRepository = new UserRepository();
            var login = "testLogin@home.pl" + DateTime.Now.ToLongTimeString();
            var user = new User { Login = login };
            var name = "Jan Kowalski";
            var pass = "polska";

            // Act
            userRepository.Save(user);
            user.Name = name;
            user.Password = pass;
            userRepository.Save(user);

            // Assert
            var loadedUser = userRepository.GetByLogin(login);
            Assert.IsNotNull(loadedUser);
            Assert.AreEqual(name, loadedUser.Name);
            Assert.AreEqual(pass, loadedUser.Password);
        }
    }
}
