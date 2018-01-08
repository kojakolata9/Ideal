using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ideal.Services
{
    public interface IUserService : IService
    {
        Task CreateAsync(
            string name,
            string id);
    }
}
