using System.Collections.ObjectModel;
using System.Windows;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.Factories;
using HybridTaskManager.LogSystem;
using NLog.LayoutRenderers;

namespace HybridTaskManager.UserConrols.TaskManageControls
{
    public partial class ManageExistingTaskControl : System.Windows.Controls.UserControl
    {
        public ObservableCollection<Project> Projects { get; set; } = new(ProjectData.Projects);
        public ObservableCollection<HybridTaskManager.DTO.DictionaryEntity.TaskStatus> Statuses { get; set; } = new(StatusTypeData.TaskStatuses);
        public ObservableCollection<TaskPriority> Priorities { get; set; } = new(TaskPriorityData.TaskPriorities);
        public ObservableCollection<TaskType> Types { get; set; } = new(TaskTypesData.TaskTypes);
        public ObservableCollection<User> Users { get; set; } = new(UserData.Users);

        public bool IsCreate = false;
        public bool IsEdit = false;
        public TaskItem CurrentTask;
        public User CurrentUser;

        public ManageExistingTaskControl(TaskItem task,User user)
        {
            InitializeComponent();
            DataContext = task;
            IsEdit = true;
            CurrentTask = task;
            CurrentUser = user;
        }

        public ManageExistingTaskControl(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            IsCreate = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit) 
            {
                if (DataContext is TaskItem editedTask)
                {
                    var existingTask = TaskData.TaskBase.FirstOrDefault(t => t.Id == editedTask.Id); 
                    if (existingTask == null)
                        {
                            System.Windows.MessageBox.Show("Задача не найдена в базе");
                            return;
                        }
                    existingTask.Title = editedTask.Title;
                    existingTask.Description = editedTask.Description;
                    existingTask.Project = editedTask.Project;
                    existingTask.Status = editedTask.Status;
                    existingTask.Type = editedTask.Type;
                    existingTask.Priority = editedTask.Priority;
                    existingTask.AssignedTo = editedTask.AssignedTo;
                    existingTask.StartAt = editedTask.StartAt;
                    existingTask.DeadLine = editedTask.DeadLine;
                    existingTask.Tags = editedTask.Tags;

                    System.Windows.MessageBox.Show("Задача сохранена");
                    AppLogger.Info($"Задача обновлена: {existingTask.Title} (ID: {existingTask.Id})");
                    CloseWindow(true);
                }
            }
            if (IsCreate)
            {
                CastomTaskFactory taskFactory = new CastomTaskFactory();
                if(CurrentUser == null)
                {
                    CurrentUser =  new User("Вы", new UserRole("Администратор", true, true));
                }
                if ( TaskName.Text == string.Empty || 
                    (TaskDescription.Text == null || TaskDescription.Text == string.Empty) ||
                    ProjectBelonging.SelectedItem == null ||
                    ChosenTaskStatus.SelectedItem == null ||
                    TasksType.SelectedItem == null ||
                    TasksPriority.SelectedItem == null ||
                    TaskPerformer.SelectedItem == null ||
                    BeginDate.SelectedDate == null ||
                    DeadLine.SelectedDate == null)
                {// ПЕРЕДЕЛАТЬ
                    MessageBox.Show("Пожалуйста, заполните все поля перед сохранением задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newTask = taskFactory.CreateTask
                    (
                        TaskName.Text,
                        TaskDescription.Text,
                        ProjectBelonging.SelectedItem as Project,
                        ChosenTaskStatus.SelectedItem as DTO.DictionaryEntity.TaskStatus,
                        TasksType.SelectedItem as TaskType,
                        TasksPriority.SelectedItem as TaskPriority,
                        CurrentUser,
                        TaskPerformer.SelectedItem as User,
                        BeginDate.SelectedDate.Value,
                        DeadLine.SelectedDate.Value
                    );
                TaskData.TaskBase.Add( newTask );
                AppLogger.Info($"Создана новая задача: {newTask.Title} (ID: {newTask.Id})");
                CloseWindow(true);
            }
        }

        private void CloseWindow(bool result)
        {
            Window parentWindow = Window.GetWindow( this );
            {
                if (parentWindow is Window dialog && dialog.IsLoaded && dialog.IsActive)
                {
                    dialog.DialogResult = result;
                }
                parentWindow.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AppLogger.Info("Отмена создания/редактирования задачи");
            CloseWindow(false);
        }
    }
}
