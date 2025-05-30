using System.Collections.ObjectModel;
using System.Windows;
using HybridTaskManager.DTO.DictionaryEntity;
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;

namespace HybridTaskManager.UserConrols
{
    public partial class KanbanBoardControl : System.Windows.Controls.UserControl
    {
        public ObservableCollection<StatusColumnViewModel> StatusColumns { get; } = new ObservableCollection<StatusColumnViewModel>();

        public KanbanBoardControl()
        {
            InitializeComponent();
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
