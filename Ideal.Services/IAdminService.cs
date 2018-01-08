using System;
using System.Collections.Generic;
using System.Text;

namespace Ideal.Services
{
    public interface IAdminService : IService
    {
        void Approve(int ideaId);
    }
}
