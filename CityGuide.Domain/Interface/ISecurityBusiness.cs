using CityGuide.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityGuide.Domain.Interface
{
    public interface ISecurityBusiness : IRepository<User>
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> IsUserExists(string userName);
    }
}
