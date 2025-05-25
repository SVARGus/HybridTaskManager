using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity
{
    public class User : INotifyPropertyChanged
    {
        private Guid _id;
        private string _userName;
        private Guid _roleId;
        private UserRole _role;
        private ObservableCollection<UserProject> _projects = new();

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

        public string UserName
        {
            get => _userName;
            set { if (_userName != value) { _userName = value; OnPropertyChanged(); } }
        }

        public Guid RoleId
        {
            get => _roleId;
            set { if (_roleId != value) { _roleId = value; OnPropertyChanged(); } }
        }

        public UserRole Role
        {
            get => _role;
            set { if (_role != value) { _role = value; OnPropertyChanged(); } }
        }

        public ObservableCollection<UserProject> Projects
        {
            get => _projects;
            set { if (_projects != value) { _projects = value; OnPropertyChanged(); } }
        }

        public User()
        {
            Id = Guid.NewGuid();
            Projects = new ObservableCollection<UserProject>();
        }

        public User(string userName, UserRole role) : this()
        {
            UserName = userName;
            Role = role;
            RoleId = role.Id;
        }

        public User(string userName, UserRole role, ObservableCollection<UserProject> userProjects) : this(userName, role)
        {
            Projects = userProjects ?? new ObservableCollection<UserProject>();
        }
    }
}
