using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;

namespace HybridTaskManager.DTO.ProjectsAndProjectRoles
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ObservableCollection<UserProject> Users { get; set; } = new();
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public Project(string prjectName, ObservableCollection<UserProject> users, ObservableCollection<TaskItem> tasks)
        {
            Id = Guid.NewGuid();
            Name = prjectName;
            Users = users;
            Tasks = tasks;
        }
    }
}