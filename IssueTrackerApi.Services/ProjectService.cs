using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;

namespace IssueTrackerApi.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository { get; set; }

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool TryAddProject(Project project)
        {
            var existingProject = _projectRepository.GetAll().FirstOrDefault(_ => _.Name == project.Name);

            if (existingProject != null)
            {
                return false;
            }

            _projectRepository.Save(project);

            return true;
        }
    }
}