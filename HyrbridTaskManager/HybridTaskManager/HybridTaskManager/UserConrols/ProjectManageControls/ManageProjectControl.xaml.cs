using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.Factories;

namespace HybridTaskManager.UserConrols.ProjectManageControls
{
    public partial class ManageProjectControl : UserControl
    {
        public ObservableCollection<SelectableUser> SelectableUsers { get; set; }

        public ManageProjectControl()
        {
            InitializeComponent();

            // Заполняем SelectableUsers из исходных данных пользователей
            SelectableUsers = new ObservableCollection<SelectableUser>(
                UserData.Users.Select(u => new SelectableUser(u)));

            DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow(false);
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProjectNameTextBox.Text))
            {
                MessageBox.Show("Введите название проекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var factory = new CastomProjectFactory();

            var newProject = factory.CreateBase(ProjectNameTextBox.Text, new ObservableCollection<UserProject>(
                SelectableUsers
                    .Where(u => u.IsChecked && u.SelectedProjectRole != null)
                    .Select(u => new UserProject(
                        new User { Id = u.Id, UserName = u.UserName },
                        /* Проект будет создан внутри фабрики, так что тут null или создавай объект для передачи */
                        null,
                        u.SelectedProjectRole))
            ));

            // Далее можно обработать newProject или закрыть окно
            CloseWindow(true);
        }

        private void CloseWindow(bool result)
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.DialogResult = result;
                parentWindow.Close();
            }
        }
    }
}
