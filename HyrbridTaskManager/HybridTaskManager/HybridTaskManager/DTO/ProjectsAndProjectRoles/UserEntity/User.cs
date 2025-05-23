using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public Guid RoleId { get; set; }
        public UserRole Role { get; set; }

        public ObservableCollection<UserProject> Projects { get; set; } = new();

        public User()
        {
            
        }

        public User(string userName, UserRole role)
        {
            Id = Guid.NewGuid();
            RoleId = role.Id;
        }
        public User(string userName, UserRole role, ObservableCollection<UserProject> userProjects)
        {
            Id = Guid.NewGuid();
            RoleId = role.Id;
            Projects = userProjects;
        }
    }
}