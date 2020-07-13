using BonitasRestClient.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitasRestClient.Contracts
{
    public interface IIdentityClient
    {
        Task<User> GetUserAsync(long userId);
        User GetUser(long userId);
        Task<IList<User>> GetUsersAsync(String filter);
        IList<User> GetUsers(String filter);
    }
}
