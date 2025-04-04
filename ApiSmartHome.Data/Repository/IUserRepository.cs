using ApiSmartHome.Data.Models;
namespace ApiSmartHome.Data.Repository
{
    public interface IUserRepository
    {
        Task<User[]> GetUser();
    }
}
