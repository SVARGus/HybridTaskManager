using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.Factories.Interfaces;

namespace HybridTaskManager.Factories
{
    public class UserFactory: IUserFactory
    {
        public User CreateUser(string username, UserRole role)
        {
            return new User(username, role);
        }

        public User CreateUser(string username, UserRole role, ObservableCollection<UserProject> userProjects)
        {
            return new User(username, role, userProjects);
        }

        public UserProject CreateUserProject(User user, Project project, ProjectRole projectRole)
        {
            return new UserProject(user, project, projectRole);
        }
    }
}
