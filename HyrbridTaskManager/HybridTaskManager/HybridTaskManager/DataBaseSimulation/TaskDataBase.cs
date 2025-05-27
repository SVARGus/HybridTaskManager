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
    new TaskItem(
        "Задача 1",
        "Описание задачи 1",
        ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[0],
        TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[0],
        UserDataBase.Users[0],
        UserDataBase.Users[1],
        DateTime.Today,
        DateTime.Today.AddHours(18)),

    new TaskItem(
        "Задача 2",
        "Описание задачи 2",
        ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[1],
        TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[1],
        UserDataBase.Users[1],
        UserDataBase.Users[2],
        DateTime.Today.AddDays(1),
        DateTime.Today.AddDays(1).AddHours(19)),

    new TaskItem(
        "Задача 3",
        "Описание задачи 3",
        ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[2],
        TaskTypesDataBase.TaskTypes[2],
        TaskPriorityDataBase.TaskPriorities[2],
        UserDataBase.Users[2],
        UserDataBase.Users[0],
        DateTime.Today.AddDays(2),
        DateTime.Today.AddDays(2).AddHours(20)),

    new TaskItem(
        "Задача 4",
        "Описание задачи 4",
        ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[0],
        TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[1],
        UserDataBase.Users[0],
        UserDataBase.Users[2],
        DateTime.Today.AddDays(3),
        DateTime.Today.AddDays(3).AddHours(17)),

    new TaskItem(
        "Задача 5",
        "Описание задачи 5",
        ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[1],
        TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[0],
        UserDataBase.Users[1],
        UserDataBase.Users[0],
        DateTime.Today.AddDays(4),
        DateTime.Today.AddDays(4).AddHours(18)),

    new TaskItem(
        "Задача 6",
        "Описание задачи 6",
        ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[2],
        TaskTypesDataBase.TaskTypes[2],
        TaskPriorityDataBase.TaskPriorities[2],
        UserDataBase.Users[2],
        UserDataBase.Users[1],
        DateTime.Today.AddDays(5),
        DateTime.Today.AddDays(5).AddHours(19)),

    new TaskItem(
        "Задача 7",
        "Описание задачи 7",
        ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[0],
        TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[1],
        UserDataBase.Users[0],
        UserDataBase.Users[2],
        DateTime.Today.AddDays(6),
        DateTime.Today.AddDays(6).AddHours(20)),
};
    }
}
