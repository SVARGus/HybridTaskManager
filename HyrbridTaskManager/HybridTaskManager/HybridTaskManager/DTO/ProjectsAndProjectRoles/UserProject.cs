using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DTO.ProjectsAndProjectRoles
{
    public class UserProject
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }

        public UserProject()
        {

        }

        public UserProject(User user,Project project, ProjectRole projectRole)
        {
            UserId = user.Id;
            ProjectId = project.Id;
            ProjectRoleId = projectRole.Id;

            User = user;
            Project = project;
            ProjectRole = projectRole;
        }
    }
}