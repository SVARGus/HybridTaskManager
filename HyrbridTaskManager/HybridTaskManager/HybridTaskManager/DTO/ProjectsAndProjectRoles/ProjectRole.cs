namespace HybridTaskManager.DTO.ProjectsAndProjectRoles
{
    public class ProjectRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // например: "Разработчик", "Аналитик", "Тестировщик"
        public bool CanEditTasks { get; set; }
        public bool CanAssignTasks { get; set; }
        public bool CanManageProjectMembers { get; set; }

        public ProjectRole()
        {
            
        }
        public ProjectRole(string name,bool EditTasksPermission, bool AssingTaskPermission, bool ManagePojectMempersPermission)
        {
            Id = Guid.NewGuid();
            Name = name;
            CanAssignTasks = EditTasksPermission;
            CanAssignTasks = AssingTaskPermission;
            CanManageProjectMembers = ManagePojectMempersPermission;
        }
    }

}