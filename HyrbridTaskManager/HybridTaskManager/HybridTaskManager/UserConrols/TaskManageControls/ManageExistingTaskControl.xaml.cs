using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.Factories;

namespace HybridTaskManager.UserConrols.TaskManageControls
{
    public partial class ManageExistingTaskControl : UserControl
    {
        public ObservableCollection<Project> Projects { get; set; } = new(ProjectDataBase.Projects);
        public ObservableCollection<HybridTaskManager.DTO.DictionaryEntity.TaskStatus> Statuses { get; set; } = new(StatusTypeDataBase.TaskStatuses);
        public ObservableCollection<TaskPriority> Priorities { get; set; } = new(TaskPriorityDataBase.TaskPriorities);
        public ObservableCollection<TaskType> Types { get; set; } = new(TaskTypesDataBase.TaskTypes);
        public ObservableCollection<User> Users { get; set; } = new(UserDataBase.Users);

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
                    var existingTask = TaskDataBase.TaskBase.FirstOrDefault(t => t.Id == editedTask.Id); 
                    if (existingTask == null)
                        {
                            MessageBox.Show("Задача не найдена в базе");
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

                    MessageBox.Show("Задача сохранена");
                }
            }
            if (IsCreate)
            {
                CastomTaskFactory taskFactory = new CastomTaskFactory();
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
            }
        }
    }
}
