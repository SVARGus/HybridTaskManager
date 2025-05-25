using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.Factories.Interfaces
{
    public interface IUserFactory
    {
        public interface IUserFactory
        {
            User CreateUser(string name, string email, string passwordHash);
        }
    }
}
