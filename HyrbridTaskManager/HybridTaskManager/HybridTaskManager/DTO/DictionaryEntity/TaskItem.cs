using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskItem
    {
        // Уникальный ID задачи, во избежание коллизии
        public Guid Id;

        // Название задачи
        public string Title;

        // Описание задачи
        public string Description;

        // ID проекта, за которым задача закреплена
        public Guid ProjectId;

        // Ссылка на сущность проекта
        public Project Project;

        // ID статуса задачи
        public Guid StatusId;

        // Ссылка на сущность статуса
        public TaskStatus Status;

        // ID Типа задачи
        public Guid TypeId;

        // Ссылка на сущность типа задачи
        public TaskType Type;

        // ID приоритета задачи
        public Guid PriorityId;

        // Ссылка на сущность приоритета задачи
        public TaskPriority Priority;

        // Перечень тегов, закреплённых за задачей
        public ObservableCollection<TaskTag> Tags = new();

        // ID пользователя, которым была назначена задача
        public Guid AssignedById;

        // Ссылка на сущность пользователя, которым была назначена задача
        public User AssignedBy;

        // ID пользователя, которому была назначена задача
        public Guid AssignedToId;

        // Ссылка на сущность пользователя, которому была назначена задача
        public User AssignedTo;

        // Время создания задачи
        public DateTime CreatedAt = DateTime.UtcNow;

        // Время начала исполнения задачи
        public DateTime StartAt;

        // Назначенное время исполнения задачи
        public DateTime DeadLine;

        // Фактическое время завершения задачи
        public DateTime FinishAt;

        // Время обновления статуса задачи
        public ObservableCollection<DateTime?> UpdatedAt = new();

        public TaskItem() { }

        // Полный конструктор
        public TaskItem(
            Guid id,
            string title,
            string description,
            Guid projectId,
            Project project,
            Guid statusId,
            TaskStatus status,
            Guid typeId,
            TaskType type,
            Guid priorityId,
            TaskPriority priority,
            ObservableCollection<TaskTag> tags,
            Guid assignedById,
            User assignedBy,
            Guid assignedToId,
            User assignedTo,
            DateTime createdAt,
            DateTime startAt,
            DateTime deadLine,
            DateTime finishAt,
            ObservableCollection<DateTime?> updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            ProjectId = projectId;
            Project = project;
            StatusId = statusId;
            Status = status;
            TypeId = typeId;
            Type = type;
            PriorityId = priorityId;
            Priority = priority;
            Tags = tags ?? new();
            AssignedById = assignedById;
            AssignedBy = assignedBy;
            AssignedToId = assignedToId;
            AssignedTo = assignedTo;
            CreatedAt = createdAt;
            StartAt = startAt;
            DeadLine = deadLine;
            FinishAt = finishAt;
            UpdatedAt = updatedAt ?? new();
        }

        // Перегрузка конструктора с получением данных из объектов
        public TaskItem(
            string title,
            string description,
            Project project,
            TaskStatus status,
            TaskType type,
            TaskPriority priority,
            User assignedBy,
            User assignedTo,
            DateTime startAt,
            DateTime deadLine)
        {
            Id = Guid.NewGuid();               
            Title = title;
            Description = description;

            Project = project;
            ProjectId = project.Id;

            Status = status;
            StatusId = status.Id;

            Type = type;
            TypeId = type.Id;

            Priority = priority;
            PriorityId = priority.Id;

            AssignedBy = assignedBy;
            AssignedById = assignedBy.Id;

            AssignedTo = assignedTo;
            AssignedToId = assignedTo.Id;

            CreatedAt = DateTime.UtcNow;
            StartAt = startAt;
            DeadLine = deadLine;

            Tags = new ObservableCollection<TaskTag>();
            UpdatedAt = new ObservableCollection<DateTime?>();
        }
    }
}
