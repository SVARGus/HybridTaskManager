using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.Factories.Interfaces
{
    public interface ITaskFacroty
    {
        public interface ITaskFactory
        {
            TaskItem CreateTask(
                string title,
                string description,
                Project project,
                DTO.DictionaryEntity.TaskStatus status,
                TaskType type,
                TaskPriority priority,
                User assignedBy,
                User assignedTo,
                DateTime startAt,
                DateTime deadline
            );
        }
    }
}
