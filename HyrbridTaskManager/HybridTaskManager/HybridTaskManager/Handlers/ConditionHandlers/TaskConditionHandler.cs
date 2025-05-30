using System;
using System.Collections.Generic;
using System.Linq;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.Handlers.ConditionHandlers
{
    public class TaskConditionHandler
    {
        // Список задач, которые находятся на контроле
        private List<TaskItem> CheckupTasks = new List<TaskItem>();

        public TaskConditionHandler()
        {
            
        }

        // Конструктор для одной задачи
        public TaskConditionHandler(TaskItem task)
        {
            AddOrReplaceTask(task);
        }

        // Конструктор для набора задач
        public TaskConditionHandler(IEnumerable<TaskItem> tasks)
        {
            foreach (var task in tasks)
            {
                AddOrReplaceTask(task);
            }
        }

        // Добавление или обновление задачи в списке
        private void AddOrReplaceTask(TaskItem task)
        {
            if (task == null) return;

            var existingIndex = CheckupTasks.FindIndex(t => t.Id == task.Id);
            if (existingIndex >= 0)
                CheckupTasks[existingIndex] = task;
            else
                CheckupTasks.Add(task);
        }

        // Проверка: есть ли исполнитель
        public bool HasAssignee(TaskItem task) =>
            task?.AssignedTo != null && task.AssignedToId != Guid.Empty;

        // Проверка: есть ли статус
        public bool HasStatus(TaskItem task) =>
            task?.Status != null && task.StatusId != Guid.Empty;

        // Проверка: установлен ли дедлайн
        public bool HasDeadline(TaskItem task) =>
            task?.DeadLine != default;

        // Проверка: есть ли теги
        public bool HasTags(TaskItem task) =>
            task?.Tags != null && task.Tags.Any();

        // Получение текстового статуса
        public string GetStatusText(TaskItem task) =>
            HasStatus(task) ? task.Status.Name : "Нет статуса";

        // UI-резюме задачи
        public string GetTaskSummary(TaskItem task) =>
            $"{task.Title} | Статус: {GetStatusText(task)} | Назначена: {task.CreatedAt:d}";

        // Проверка на просрочку
        public bool IsOverdue(TaskItem task) =>
            task != null && task.DeadLine != default && task.DeadLine < DateTime.UtcNow && task.FinishAt == default;

        // Обновление дедлайна и синхронизация
        public void UpdateDeadline(TaskItem task, DateTime newDeadline)
        {
            if (task == null) return;
            task.DeadLine = newDeadline;
            AddOrReplaceTask(task);
        }

        // Назначение пользователя и синхронизация
        public void AssignUser(TaskItem task, User user)
        {
            if (task == null || user == null) return;
            task.AssignedTo = user;
            task.AssignedToId = user.Id;
            AddOrReplaceTask(task);
        }

        // Получение всех незакреплённых задач
        public IEnumerable<TaskItem> GetUnassignedTasks() =>
            CheckupTasks.Where(t => !HasAssignee(t));

        // Получение всех просроченных задач
        public IEnumerable<TaskItem> GetOverdueTasks() =>
            CheckupTasks.Where(t => IsOverdue(t));

        // Добавление нового тега
        public void AddTag(TaskItem task, Tag tag)
        {

        }

        // Удаление существующего тега
        public void RemoveTag(TaskItem task, Tag tag)
        {

        }

        // Проверка валидности задачи: не null, есть исполнитель, статус, дедлайн и хотя бы один тег(доработать)
        public bool IsValidTask(TaskItem task)
        {
            if (task != null
                //&& HasAssignee(task)
                //&& HasStatus(task)
                //&& HasDeadline(task))
                )
            {
                return true;
            }
            return false;       
        }
    }
}
