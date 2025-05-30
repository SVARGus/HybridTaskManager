using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.Factories.Interfaces
{
    public interface IProjectFactory
    {
        Project CreateEmpty();
        Project CreateWithName(string projectName);
        Project CreateFull(string projectName, ObservableCollection<UserProject> users, ObservableCollection<TaskItem> tasks);
    }
}
