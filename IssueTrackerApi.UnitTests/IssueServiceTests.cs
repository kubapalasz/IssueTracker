using System.Collections.Generic;
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
            var iIssueService = new IssueService(issueRepoMoq.Object);

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
            issueRepoMoq.Setup(_ => _.Get()).Returns(new List<IssueModel> { new IssueModel() });
            var iIssueService = new IssueService(issueRepoMoq.Object);

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
            issueRepoMoq.Setup(_ => _.Get()).Returns(new List<IssueModel> { new IssueModel(), new IssueModel(), new IssueModel(), new IssueModel() });
            var iIssueService = new IssueService(issueRepoMoq.Object);

            // Act
            var issues = iIssueService.Get(2, 2);

            // Assert
            Assert.NotNull(issues);
            Assert.True(issues.Count == 2);
        }
    }
}