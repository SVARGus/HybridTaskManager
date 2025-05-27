using System.Collections.ObjectModel;
using System.Text;
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
using HybridTaskManager.UserConrols.TaskManageControls;
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;

namespace HybridTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 1) базовые «справочники»
            var statusTodo = new TaskStatus("Запланировано", order: 1, colorCode: "#FFA");
            var statusInWork = new TaskStatus("В работе", order: 2, colorCode: "#AFF");
            var statusDone = new TaskStatus("Готово", order: 3, colorCode: "#AFA");

            var typeFeature = new TaskType("Feature");
            var typeBug = new TaskType("Bug");

            var prioLow = new TaskPriority("Низкий", priorityLevel: 1, colorCode: "#DDD");
            var prioHigh = new TaskPriority("Высокий", priorityLevel: 5, colorCode: "#F55");

            var tagUi = new Tag("UI");
            var tagBackend = new Tag("Backend");

            // 2) Пользователи и проект
            var roleUser = new UserRole("Пользователь", manageUsersPermission: false, manageDictionaryPermission: false);
            var userIvan = new User("Иван Иванов", roleUser);
            var userPetr = new User("Пётр Петров", roleUser);

            // 1) Сначала создаём объект Project
            var projectA = new Project("Проект A",
                users: new ObservableCollection<UserProject>(),
                tasks: new ObservableCollection<TaskItem>());

            // 2) Создаём роль в проекте
            var projectRoleDev = new ProjectRole("Разработчик", EditTasksPermission: true, AssingTaskPermission: true, ManagePojectMempersPermission: false);

            // 3) Теперь безопасно связываем User и Project
            var upIvan = new UserProject(userIvan, projectA, projectRoleDev);
            var upPetr = new UserProject(userPetr, projectA, projectRoleDev);

            // 4) Добавляем связи в коллекцию проекта
            projectA.Users.Add(upIvan);
            projectA.Users.Add(upPetr);

            // back-референс
            foreach (var up in projectA.Users) up.Project = projectA;

            // 3) Тестовые задачи
            var task1 = new TaskItem(
                title: "Нарисовать мокап",
                description: "Главный экран",
                project: projectA,
                status: statusTodo,
                type: typeFeature,
                priority: prioLow,
                assignedBy: userIvan,
                assignedTo: userPetr,
                startAt: DateTime.Today,
                deadLine: DateTime.Today.AddDays(3)
            );
            task1.Tags.Add(new TaskTag(task1, new ObservableCollection<Tag> { tagUi }));

            var task2 = new TaskItem(
                title: "Починить API",
                description: "Ошибка 500 на эндпоинте /tasks",
                project: projectA,
                status: statusInWork,
                type: typeBug,
                priority: prioHigh,
                assignedBy: userPetr,
                assignedTo: userIvan,
                startAt: DateTime.Today.AddDays(-1),
                deadLine: DateTime.Today.AddDays(1)
            );
            task2.Tags.Add(new TaskTag(task2, new ObservableCollection<Tag> { tagBackend }));

            var task3 = new TaskItem(
                title: "Тестовый релиз",
                description: "Версия 1.0 готова",
                project: projectA,
                status: statusDone,
                type: typeFeature,
                priority: prioLow,
                assignedBy: userIvan,
                assignedTo: userIvan,
                startAt: DateTime.Today.AddDays(-5),
                deadLine: DateTime.Today.AddDays(-1)
            );
            task3.Tags.Add(new TaskTag(task3, new ObservableCollection<Tag> { tagUi, tagBackend }));

            // 4) Сборка в колонки
            var columns = new ObservableCollection<object>
            {
                new { Status = statusTodo,   Tasks = new ObservableCollection<TaskItem> { task1 } },
                new { Status = statusInWork, Tasks = new ObservableCollection<TaskItem> { task2 } },
                new { Status = statusDone,   Tasks = new ObservableCollection<TaskItem> { task3 } }
            };

            // 5) Привязка к KanbanBoardControl
            KanbanBoard.DataContext = new { StatusColumns = columns };

            // 6) Заполнение CalendarUI на примере CalendarControl.LoadTasks(new[] { task1, task2, task3 });
        }
    }
}
