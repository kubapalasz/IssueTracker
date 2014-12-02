using System.Collections.Generic;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using IssueTrackerApi.Services;
using Moq;
using Xunit;

namespace IssueTrackerApi.UnitTests
{
    public class ProjectServiceTests
    {
        [Fact]
        public void TryAddProjectSucceedsNoSameProjects()
        {
            // Arrange
            var projectRepoMoq = new Mock<IProjectRepository>();
            projectRepoMoq.Setup(_ => _.GetAll()).Returns(() => new List<Project>());

            var iProjectService = new ProjectService(projectRepoMoq.Object);

            // Act
            var result = iProjectService.TryAddProject(new Project());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TryAddProjectFailsDueToSameProjects()
        {
            // Arrange
            var projectRepoMoq = new Mock<IProjectRepository>();
            const string projectName = "Test";
            projectRepoMoq.Setup(_ => _.GetAll()).Returns(() => new List<Project> {new Project {Name = projectName}});

            var iProjectService = new ProjectService(projectRepoMoq.Object);

            // Act
            var result = iProjectService.TryAddProject(new Project {Name = projectName});

            // Assert
            Assert.False(result);
        }
    }
}