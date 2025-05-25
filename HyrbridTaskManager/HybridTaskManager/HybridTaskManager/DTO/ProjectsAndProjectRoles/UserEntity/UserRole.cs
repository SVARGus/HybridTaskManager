namespace HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // например, "Администратор", "Пользователь"
        public bool CanManageUsers { get; set; }
        public bool CanManageDictionaries { get; set; } // управлять типами, тегами и т.д.

        public UserRole() { }

        public UserRole(string userRoleName, bool manageUsersPermission, bool manageDictionaryPermission)
        {
            Id = Guid.NewGuid();
            Name = userRoleName;
            CanManageUsers = manageUsersPermission;
            CanManageDictionaries = manageDictionaryPermission;
            Name = string.Empty;
        }
    }
}
