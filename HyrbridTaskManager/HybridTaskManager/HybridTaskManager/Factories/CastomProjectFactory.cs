using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.Factories.Interfaces;

namespace HybridTaskManager.Factories
{
    public class CastomProjectFactory : IProjectFactory
    {
        public Project CreateEmpty()
        {
            return new Project
            {
                Id = Guid.NewGuid(),
                Name = "Новый проект",
                Users = new ObservableCollection<UserProject>(),
                Tasks = new ObservableCollection<TaskItem>()
            };
        }

        public Project CreateWithName(string projectName)
        {
            return new Project(projectName);
        }

        public Project CreateFull(string projectName, ObservableCollection<UserProject> users, ObservableCollection<TaskItem> tasks)
        {
            return new Project(projectName, users, tasks);
        }

        public Project CreateBase(string projectName, ObservableCollection<UserProject> users)
        {
            return new Project(projectName, users);
        }
    }
}
