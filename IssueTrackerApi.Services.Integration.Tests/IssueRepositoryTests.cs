using System;
using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueTrackerApi.Services.Integration.Tests
{
    [TestClass]
    public class IssueRepositoryTests
    {
        [TestMethod]
        public void IssueIsSavedAndNewIdIsSet()
        {
            // Arrange
            IIssueRepository issueRepository = new IssueRepository();
            var issueModel = new IssueModel();

            // Act
            var issueId = issueRepository.Save(issueModel);

            // Assert
            Assert.IsTrue(issueId > 0);
        }

        [TestMethod]
        public void IssueIsSavedAndFieldsAreCorrectlyStored()
        {
            // Arrange
            IIssueRepository issueRepository = new IssueRepository();
            var title = "Fake issue" + DateTime.Now.ToLongTimeString();
            var status = Statuses.Open;
            var issueModel = new IssueModel
            {
                Status = status,
                Title = title
            };

            // Act
            var issueId = issueRepository.Save(issueModel);

            // Assert
            var allIssues = issueRepository.Get();
            Assert.IsNotNull(allIssues);
            var savedIssue = allIssues.SingleOrDefault(_ => _.Id == issueId);
            Assert.IsNotNull(savedIssue);
            Assert.AreEqual(title, savedIssue.Title);
            Assert.AreEqual(status, savedIssue.Status);
        }

    }
}
