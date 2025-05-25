using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class ProjectDataBase
    {
        public static ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>
        {
            new Project(
                "Test Project 1",
                new ObservableCollection<UserProject>(),
                new ObservableCollection<DTO.DictionaryEntity.TaskItem>()
            ),
                new Project(
                "Test Project 2",
                new ObservableCollection<UserProject>(),
                new ObservableCollection<DTO.DictionaryEntity.TaskItem>()
            ),
                new Project(
                "Test Project 3",
                new ObservableCollection<UserProject>(),
                new ObservableCollection<DTO.DictionaryEntity.TaskItem>()
            )
        };
    }
}
