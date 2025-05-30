using System.ComponentModel;
using System.Runtime.CompilerServices;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.UserConrols.ProjectManageControls
{
    public class SelectableUser : INotifyPropertyChanged
    {
        private bool _isChecked;
        private ProjectRole _selectedProjectRole;

        public Guid Id { get; set; }
        public string UserName { get; set; }

        // Проектная роль, которую можно менять
        public ProjectRole SelectedProjectRole
        {
            get => _selectedProjectRole;
            set
            {
                if (_selectedProjectRole != value)
                {
                    _selectedProjectRole = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public SelectableUser(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            // Можно задать дефолтную роль, если нужно
            SelectedProjectRole = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
