using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
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
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;

namespace HybridTaskManager.UserConrols
{
    public partial class KanbanBoardControl : UserControl
    {
        public ObservableCollection<StatusColumnViewModel> StatusColumns { get; } = new ObservableCollection<StatusColumnViewModel>();

        public KanbanBoardControl()
        {
            InitializeComponent();
            LoadTestData();
        }

        private void LoadTestData()
        {
            //var groupedTasks = TaskData.TaskBase
            //    .GroupBy(t => t.Status.Id)
            //    .ToDictionary(g => g.Key, g => g.ToList());

            //foreach (var status in StatusTypeData.TaskStatuses)
            //{
            //    if (groupedTasks.TryGetValue(status.Id, out var tasks))
            //    {
            //        StatusColumns.Add(new StatusColumnViewModel(status, tasks));
            //    }
            //    else
            //    {
            //        StatusColumns.Add(new StatusColumnViewModel(status, new List<TaskItem>()));
            //    }
            //}
        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Добавить новую колонку в доску
        }

        private void RefreshBoard_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Обновить данные всей доски
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Открыть окно настроек доски
        }

        public class StatusColumnViewModel
        {
            public TaskStatus Status { get; }
            public List<TaskItem> Tasks { get; }

            public StatusColumnViewModel(TaskStatus status, List<TaskItem> tasks)
            {
                Status = status;
                Tasks = tasks;
            }
        }
    }
}
