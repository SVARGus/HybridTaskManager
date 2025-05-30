using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskItem : INotifyPropertyChanged
    {
        private Guid _id;
        private string _title;
        private string _description;
        private Guid _projectId;
        private Project _project;
        private Guid _statusId;
        private TaskStatus _status;
        private Guid _typeId;
        private TaskType _type;
        private Guid _priorityId;
        private TaskPriority _priority;
        private ObservableCollection<TaskTag> _tags;
        private Guid _assignedById;
        private User _assignedBy;
        private Guid _assignedToId;
        private User _assignedTo;
        private DateTime _createdAt;
        private DateTime _startAt;
        private DateTime _deadLine;
        private DateTime _finishAt;
        private ObservableCollection<DateTime?> _updatedAt;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid Id
        {
            get => _id;
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        public string Title
        {
            get => _title;
            set { if (_title != value) { _title = value; OnPropertyChanged(); } }
        }

        public string Description
        {
            get => _description;
            set { if (_description != value) { _description = value; OnPropertyChanged(); } }
        }

        public Guid ProjectId
        {
            get => _projectId;
            set { if (_projectId != value) { _projectId = value; OnPropertyChanged(); } }
        }

        public Project Project
        {
            get => _project;
            set { if (_project != value) { _project = value; OnPropertyChanged(); } }
        }

        public Guid StatusId
        {
            get => _statusId;
            set { if (_statusId != value) { _statusId = value; OnPropertyChanged(); } }
        }

        public TaskStatus Status
        {
            get => _status;
            set { if (_status != value) { _status = value; OnPropertyChanged(); } }
        }

        public Guid TypeId
        {
            get => _typeId;
            set { if (_typeId != value) { _typeId = value; OnPropertyChanged(); } }
        }

        public TaskType Type
        {
            get => _type;
            set { if (_type != value) { _type = value; OnPropertyChanged(); } }
        }

        public Guid PriorityId
        {
            get => _priorityId;
            set { if (_priorityId != value) { _priorityId = value; OnPropertyChanged(); } }
        }

        public TaskPriority Priority
        {
            get => _priority;
            set { if (_priority != value) { _priority = value; OnPropertyChanged(); } }
        }

        public ObservableCollection<TaskTag> Tags
        {
            get => _tags;
            set { if (_tags != value) { _tags = value; OnPropertyChanged(); } }
        }

        public Guid AssignedById
        {
            get => _assignedById;
            set { if (_assignedById != value) { _assignedById = value; OnPropertyChanged(); } }
        }

        public User AssignedBy
        {
            get => _assignedBy;
            set { if (_assignedBy != value) { _assignedBy = value; OnPropertyChanged(); } }
        }

        public Guid AssignedToId
        {
            get => _assignedToId;
            set { if (_assignedToId != value) { _assignedToId = value; OnPropertyChanged(); } }
        }

        public User AssignedTo
        {
            get => _assignedTo;
            set { if (_assignedTo != value) { _assignedTo = value; OnPropertyChanged(); } }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set { if (_createdAt != value) { _createdAt = value; OnPropertyChanged(); } }
        }

        public DateTime StartAt
        {
            get => _startAt;
            set { if (_startAt != value) { _startAt = value; OnPropertyChanged(); } }
        }

        public DateTime DeadLine
        {
            get => _deadLine;
            set { if (_deadLine != value) { _deadLine = value; OnPropertyChanged(); } }
        }

        public DateTime FinishAt
        {
            get => _finishAt;
            set { if (_finishAt != value) { _finishAt = value; OnPropertyChanged(); } }
        }

        public ObservableCollection<DateTime?> UpdatedAt
        {
            get => _updatedAt;
            set { if (_updatedAt != value) { _updatedAt = value; OnPropertyChanged(); } }
        }

        // Конструкторы
        public TaskItem()
        {
            Tags = new ObservableCollection<TaskTag>();
            UpdatedAt = new ObservableCollection<DateTime?>();
            CreatedAt = DateTime.UtcNow;
        }

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
            ObservableCollection<DateTime?> updatedAt) : this()
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
            Tags = tags ?? new ObservableCollection<TaskTag>();
            AssignedById = assignedById;
            AssignedBy = assignedBy;
            AssignedToId = assignedToId;
            AssignedTo = assignedTo;
            CreatedAt = createdAt;
            StartAt = startAt;
            DeadLine = deadLine;
            FinishAt = finishAt;
            UpdatedAt = updatedAt ?? new ObservableCollection<DateTime?>();
        }

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
            DateTime deadLine) : this()
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

            StartAt = startAt;
            DeadLine = deadLine;
        }
    }
}
