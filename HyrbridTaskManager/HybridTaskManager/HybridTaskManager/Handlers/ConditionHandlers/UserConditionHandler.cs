using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

public class UserConditionHandler
{
    public User User { get; }

    public UserConditionHandler(User user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
    }

    public bool IsUserValid() =>
        User != null && !string.IsNullOrWhiteSpace(User.UserName);

    public bool CanManageUsers() =>
        User?.Role?.CanManageUsers == true;

    public bool CanManageDictionaries() =>
        User?.Role?.CanManageDictionaries == true;

    public bool IsAssignedToProject(Guid projectId) =>
        User?.Projects.Any(up => up.ProjectId == projectId) == true;

    public bool CanManageProjectMembers(Guid projectId)
    {
        return User?.Projects
            .Any(up => up.ProjectId == projectId &&
                       up.ProjectRole?.CanManageProjectMembers == true) == true;
    }

    public bool HasAnyProjectRolePermission(Func<ProjectRole, bool> predicate)
    {
        return User?.Projects.Any(up =>
            up.ProjectRole != null && predicate(up.ProjectRole)) == true;
    }
}
