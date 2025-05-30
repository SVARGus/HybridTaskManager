using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DTO.ProjectsAndProjectRoles
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ObservableCollection<UserProject> Users { get; set; } = new();
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public Project()
        {
            
        }

        public Project(string prjectName)
        {
            Id = Guid.NewGuid();
            Name = prjectName;
        }
        public Project(string prjectName, ObservableCollection<UserProject> users, ObservableCollection<TaskItem> tasks)
        {
            Id = Guid.NewGuid();
            Name = prjectName;
            Users = users;
            Tasks = tasks;
        }

        public Project(string prjectName, ObservableCollection<UserProject> users)
        {
            Id = Guid.NewGuid();
            Name = prjectName;
            Users = users;
        }
    }
}