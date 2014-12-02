namespace IssueTrackerApi.Core.Services
{
    public interface IProjectService
    {
        bool TryAddProject(Project project);
    }
}