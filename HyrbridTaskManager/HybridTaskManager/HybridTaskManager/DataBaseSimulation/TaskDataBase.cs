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
    // ПОНЕДЕЛЬНИК

    new TaskItem("Понедельник Задача 1", "Описание", ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[0], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[0], UserDataBase.Users[0], UserDataBase.Users[1],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1), // Monday
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 4).AddHours(3)),

    new TaskItem("Понедельник Задача 2", "Описание", ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[1], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[1], UserDataBase.Users[1], UserDataBase.Users[2],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1).AddHours(6)),

    // ВТОРНИК
    new TaskItem("Вторник Задача 1", "Описание", ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[2], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[2], UserDataBase.Users[2], UserDataBase.Users[0],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2).AddHours(3)),

    new TaskItem("Вторник Задача 2", "Описание", ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[3], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[3], UserDataBase.Users[0], UserDataBase.Users[1],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 2).AddHours(6)),

    // СРЕДА
    new TaskItem("Среда Задача 1", "Описание", ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[0], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[0], UserDataBase.Users[1], UserDataBase.Users[2],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3).AddHours(3)),

    new TaskItem("Среда Задача 2", "Описание", ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[1], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[1], UserDataBase.Users[2], UserDataBase.Users[0],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 3).AddHours(6)),

    // ЧЕТВЕРГ
    new TaskItem("Четверг Задача 1", "Описание", ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[2], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[2], UserDataBase.Users[0], UserDataBase.Users[1],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 5).AddHours(3)),

    new TaskItem("Четверг Задача 2", "Описание", ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[3], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[3], UserDataBase.Users[1], UserDataBase.Users[2],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 4).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 4).AddHours(6)),

    // ПЯТНИЦА
    new TaskItem("Пятница Задача 1", "Описание", ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[0], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[0], UserDataBase.Users[2], UserDataBase.Users[0],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 5),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 5).AddHours(3)),

    new TaskItem("Пятница Задача 2", "Описание", ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[1], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[1], UserDataBase.Users[0], UserDataBase.Users[1],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 5).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 6).AddHours(6)),

    // СУББОТА
    new TaskItem("Суббота Задача 1", "Описание", ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[2], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[2], UserDataBase.Users[1], UserDataBase.Users[2],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 6),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 7).AddHours(3)),

    new TaskItem("Суббота Задача 2", "Описание", ProjectDataBase.Projects[2],
        StatusTypeDataBase.TaskStatuses[3], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[3], UserDataBase.Users[2], UserDataBase.Users[0],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 6).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 6).AddHours(6)),

    // ВОСКРЕСЕНЬЕ
    new TaskItem("Воскресенье Задача 1", "Описание", ProjectDataBase.Projects[0],
        StatusTypeDataBase.TaskStatuses[0], TaskTypesDataBase.TaskTypes[0],
        TaskPriorityDataBase.TaskPriorities[0], UserDataBase.Users[0], UserDataBase.Users[1],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 7),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 7).AddHours(3)),

    new TaskItem("Воскресенье Задача 2", "Описание", ProjectDataBase.Projects[1],
        StatusTypeDataBase.TaskStatuses[1], TaskTypesDataBase.TaskTypes[1],
        TaskPriorityDataBase.TaskPriorities[1], UserDataBase.Users[1], UserDataBase.Users[2],
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 7).AddHours(4),
        DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 9).AddHours(6)),
};
    }
}
