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
using HybridTaskManager.LogSystem;

namespace HybridTaskManager
{
    public partial class MainWindow : Window
    {
        private User CurrentUser = new User("Вы", new UserRole("Администратор", true, true));
        public MainWindow()
        {
            AppLogger.Info("----- Новая сессия: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " -----");

            AppLogger.Info("Запуск приложения HybridTaskManager");
            AppLogger.Info("Загрузка данных задач и проектов");

            AppLogger.Info("Инициализация пользовательского интерфейса");

            InitializeComponent();

            AppLogger.Info("Загрузка тестовых данных для Kanban-доски");

            ProjectsList.ItemsSource = ProjectData.Projects;
            LoadKanbanTestData();
            AppLogger.Info("Загрузка пользовательского интерфейса календаря в отдельную вкладку");
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
            // 1) Получаем тестовые задачи
            var allTasks = TaskData.TaskBase;

            // 2) Получаем статусы ровно в том порядке, в каком они в JSON
            var statuses = StatusTypeData.TaskStatuses
                .OrderBy(s => s.Order)     // на всякий случай: JSON уже содержит порядок
                .ToList();

            // 3) Строим колонки: для каждого статуса свои задачи
            var columns = statuses
                .Select(s => new StatusColumnViewModel
                {
                    Status = s,
                    Tasks = new ObservableCollection<TaskItem>(
                        allTasks.Where(t => t.StatusId == s.Id))
                })
                .ToList();

            // 4) Добавляем «пустой» столбец для создания нового статуса/колонки
            columns.Add(new StatusColumnViewModel
            {
                Status = new TaskStatus("Новый статус", statuses.Count, "#CCCCCC"),
                Tasks = new ObservableCollection<TaskItem>()
            });

            // 5) Привязываем получившийся набор к контролу
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
            }
        }

    }
}
