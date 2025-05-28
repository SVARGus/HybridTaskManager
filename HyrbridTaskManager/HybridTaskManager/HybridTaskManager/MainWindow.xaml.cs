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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadKanbanTestData();
        }

        private void LoadKanbanTestData()
        {
            var allTasks = TaskDataBase.TaskBase;

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
    }
}
