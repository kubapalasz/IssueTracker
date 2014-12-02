using System.Collections.Generic;

namespace IssueTrackerApi.Core.Services
{
    public interface IProjectRepository
    {
        List<Project> GetAll();
        void Save(Project project);
    }
}
