using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskDataBase
    {
        public static ObservableCollection<TaskItem> TaskBase = new ObservableCollection<TaskItem>
        {
            new TaskItem(   "Test Task 1",
                            "Выполнить тестовую задачу 1",
                            ProjectDataBase.Projects[0],
                            StatusTypeDataBase.TaskStatuses[0],
                            TaskTypesDataBase.TaskTypes[0],
                            TaskPriorityDataBase.TaskPriorities[0],
                            UserDataBase.Users[0],
                            UserDataBase.Users[1],
                            DateTime.Now,
                            DateTime.Today),

             new TaskItem(   "Test Task 2",
                            "Выполнить тестовую задачу 2",
                            ProjectDataBase.Projects[1],
                            StatusTypeDataBase.TaskStatuses[1],
                            TaskTypesDataBase.TaskTypes[1],
                            TaskPriorityDataBase.TaskPriorities[2],
                            UserDataBase.Users[1],
                            UserDataBase.Users[2],
                            DateTime.Now,
                            DateTime.Today),
        };
    }
}
