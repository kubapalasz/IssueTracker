using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using ServiceStack.Redis;

namespace IssueTrackerApi.Services
{
    public class UserRepository : IUserRepository
    {
        public User GetByLogin(string userLogin)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.GetTypedClient<User>();

                var user = userClient.GetAll().SingleOrDefault(_=>_.Login == userLogin);

                return user;
            }
        }

        public void Save(User user)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.GetTypedClient<User>();

                userClient.Store(user);
            }
        }
    }
}
