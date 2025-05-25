using System.Windows;
using System.Windows.Controls;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.UserConrols.TestUserContols
{
    /// <summary>
    /// Логика взаимодействия для CreationOfTaskAndUser.xaml
    /// </summary>
    public partial class CreationOfTaskAndUser : UserControl
    {
        User CurrentUser = new User("Серёга",new UserRole("Администратор",true,true));
        public CreationOfTaskAndUser()
        {
            InitializeComponent();
            ChosenTaskStatus.ItemsSource = StatusTypeDataBase.TaskStatuses;
            NewTasksType.ItemsSource = TaskTypesDataBase.TaskTypes;
            TaskPerformer.ItemsSource = UserDataBase.Users;
            CurrentTasks.ItemsSource = TaskDataBase.TaskBase;
            Belonging.ItemsSource = ProjectDataBase.Projects;
            NewTasksPriority.ItemsSource = TaskPriorityDataBase.TaskPriorities;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Factories.CastomTaskFactory factory = new Factories.CastomTaskFactory();
            TaskItem newTask = factory.CreateTask(NewTasksName.Text,
                                            TaskDescription.Text,
                                            Belonging.SelectedItem as Project,
                                            ChosenTaskStatus.SelectedItem as DTO.DictionaryEntity.TaskStatus,
                                            NewTasksType.SelectedItem as TaskType,
                                            NewTasksPriority.SelectedItem as TaskPriority,
                                            CurrentUser,
                                            TaskPerformer.SelectedItem as User,
                                            BeginDate.SelectedDate.Value,
                                            DeadLineDate.SelectedDate.Value
                                            );
            TaskDataBase.TaskBase.Add(newTask);
        }
    }
}
