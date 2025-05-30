using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.Factories.Interfaces;
using HybridTaskManager.Handlers.ConditionHandlers;

namespace HybridTaskManager.Factories
{
    public class CastomTaskFactory: ITaskFacroty
    {
        public TaskItem CreateTask(
            string title,
            string description,
            Project project,
            DTO.DictionaryEntity.TaskStatus status,
            TaskType type,
            TaskPriority priority,
            User assignedBy,
            User assignedTo,
            DateTime startAt,
            DateTime deadline)
        {
            var task = new TaskItem(
                title,
                description,
                project,
                status,
                type,
                priority,
                assignedBy,
                assignedTo,
                startAt,
                deadline
            );

            TaskConditionHandler conditionHandler = new TaskConditionHandler();
            if (!conditionHandler.IsValidTask(task))
                return null;
            return task;
        }
    }
}
