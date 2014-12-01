namespace IssueTrackerApi.Core.Services
{
    public interface IUserRepository
    {
        User GetByLogin(string userLogin);
    }
}
