using System;
using System.Collections.Generic;
using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using IssueTrackerApi.Services;
using Moq;
using Xunit;

namespace IssueTrackerApi.UnitTests
{
    public class IssueServiceTests
    {
        [Fact]
        public void GetAllIssuesReturnsNotEmptyList()
        {
            // Arrange
            var issueRepoMoq = new Mock<IIssueRepository>();
            issueRepoMoq.Setup(_ => _.Get()).Returns(new List<IssueModel> {new IssueModel()});
            var userRepoMock = new Mock<IUserRepository>();

            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);

            // Act
            var issues = iIssueService.Get();

            // Assert
            Assert.NotNull(issues);
            Assert.True(issues.Count > 0);
        }

        [Fact]
        public void Get1PageReturnsCorrectPageSizeOf1()
        {
            // Arrange
            var issueRepoMoq = new Mock<IIssueRepository>();
            issueRepoMoq.Setup(_ => _.Get()).Returns(new List<IssueModel> {new IssueModel()});
            var userRepoMock = new Mock<IUserRepository>();
            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);

            // Act
            var issues = iIssueService.Get(1, 1);

            // Assert
            Assert.NotNull(issues);
            Assert.True(issues.Count > 0);
        }

        [Fact]
        public void Get2PageReturnsCorrectPageSizeOf2()
        {
            // Arrange
            var issueRepoMoq = new Mock<IIssueRepository>();
            issueRepoMoq.Setup(_ => _.Get())
                .Returns(new List<IssueModel> {new IssueModel(), new IssueModel(), new IssueModel(), new IssueModel()});
            var userRepoMock = new Mock<IUserRepository>();
            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);

            // Act
            var issues = iIssueService.Get(2, 2);

            // Assert
            Assert.NotNull(issues);
            Assert.True(issues.Count == 2);
        }

        [Fact]
        public void GetIssueByProjectNameReturnsOnlyIssuesForThatProject()
        {
            // Arrange
            const string projectName = "TestName1";
            var issueRepoMoq = new Mock<IIssueRepository>();
            issueRepoMoq.Setup(_ => _.Get())
                .Returns(new List<IssueModel>
                {
                    new IssueModel {Project = new Project {Name = "TestName1"}},
                    new IssueModel {Project = new Project {Name = "TestName1"}},
                    new IssueModel {Project = new Project {Name = "TestName2"}},
                    new IssueModel {Project = new Project {Name = "TestName3"}}
                });
            var userRepoMock = new Mock<IUserRepository>();
            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);

            // Act
            var issues = iIssueService.GetByName(projectName);

            // Assert
            Assert.NotNull(issues);
            Assert.True(issues.All(issue => issue.Project.Name == projectName));
        }

        [Fact]
        public void AssignIssueToExistingUserSuccess()
        {
            // Arrange
            const string userLogin = "user1";
            var sampleIssue = new IssueModel
            {
                AssignedTo = new User {Login = "Login1", Name = "Name1", Password = "xxx"}
            };
            var issueRepoMoq = new Mock<IIssueRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            userRepoMock.Setup(_ => _.GetByLogin(userLogin)).Returns(new User{Login = userLogin});
            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);


            // Act
            iIssueService.AssignUser(sampleIssue, userLogin);

            // Assert
            issueRepoMoq.Verify(m => m.Save(sampleIssue), Times.Once());
            Assert.Equal(sampleIssue.AssignedTo.Login, userLogin);

        }

        [Fact]
        public void AssignIssueToNonExistingUserExceptionTrown()
        {
            // Arrange
            const string userLogin = "user1";
            const string exceptionMessage = "User doesn't exist";
            var sampleIssue = new IssueModel
            {
                AssignedTo = new User { Login = "Login1", Name = "Name1", Password = "xxx" }
            };
            var issueRepoMoq = new Mock<IIssueRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            userRepoMock.Setup(_ => _.GetByLogin(userLogin)).Returns((User)null);

            var iIssueService = new IssueService(issueRepoMoq.Object, userRepoMock.Object);


            // Act

            var ex = Assert.Throws<Exception>(() => iIssueService.AssignUser(sampleIssue, userLogin));

            // Assert
            Assert.Equal(exceptionMessage, ex.Message);
            issueRepoMoq.Verify(m => m.Save(It.IsAny<IssueModel>()), Times.Never());


        }

    }
}