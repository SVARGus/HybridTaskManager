using System.Collections.ObjectModel;
using System.Windows;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;
using HybridTaskManager.LocalSaveDataManage;
using Ookii.Dialogs.Wpf;
using HybridTaskManager.UserConrols.CalendarUI;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using System.Windows.Controls;

namespace HybridTaskManager
{
    public partial class MainWindow : Window
    {
        private User CurrentUser = new User("Вы", new UserRole("Администратор", true, true));
        public MainWindow()
        {
            InitializeComponent();
            LoadKanbanTestData();
            CurrentLocalSaveDir.DataContext = LocalDataSaveManager.CurrentLocalSaveDirecoty;

            var calendarControl = new CalendarUI(CurrentUser);

            // Находим нужный TabItem (например, по имени)
            var calendarTab = MainTabControl.Items
                .OfType<TabItem>()
                .FirstOrDefault(ti => (string)ti.Header == "Календарь");

            if (calendarTab != null)
            {
                // Устанавливаем содержимое вкладки в созданный контрол
                calendarTab.Content = calendarControl;
            }

        }

        private void LoadKanbanTestData()
        {
            var allTasks = TaskData.TaskBase;

            var statuses = allTasks
                .Select(t => t.Status)
                .GroupBy(s => s.Id)
                .Select(g => g.First())
                .OrderBy(s => s.Order)
                .ToList();

            var columns = statuses
                .Select(s => new StatusColumnViewModel
                {
                    Status = s,
                    Tasks = new ObservableCollection<TaskItem>(
                        allTasks.Where(t => t.StatusId == s.Id))
                })
                .ToList();

            columns.Add(new StatusColumnViewModel
            {
                Status = new TaskStatus("Новый статус", statuses.Count + 1, "#CCCCCC"),
                Tasks = new ObservableCollection<TaskItem>()
            });

            KanbanBoard.DataContext = new
            {
                StatusColumns = new ObservableCollection<StatusColumnViewModel>(columns)
            };
        }

        public class StatusColumnViewModel
        {
            public TaskStatus Status { get; set; }
            public ObservableCollection<TaskItem> Tasks { get; set; }
        }

        private void LocalSaveDirButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Выберите папку",
                UseDescriptionForTitle = true, // Использовать Description как заголовок окна
                ShowNewFolderButton = true // Показывать кнопку "Создать папку"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string selectedPath = dialog.SelectedPath;
                LocalDataSaveManager.CurrentLocalSaveDirecoty = selectedPath;
            }
        }
    }
}
