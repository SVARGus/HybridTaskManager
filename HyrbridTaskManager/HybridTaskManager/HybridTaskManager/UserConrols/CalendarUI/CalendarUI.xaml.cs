using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.UserConrols.TaskManageControls;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace HybridTaskManager.UserConrols.CalendarUI
{
    /// <summary>
    /// Логика взаимодействия для CalendarUI.xaml
    /// </summary>
    public partial class CalendarUI : UserControl
    {

        private bool isInitialized = false;

        public CalendarUI()
        {
            InitializeComponent();
            Loaded += CalendarUI_Loaded;

        }

        private List<TaskItem> GetTasksForWeek(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(6);
            try
            {
                return TaskDataBase.TaskBase
                    .Where(task => task.StartAt.Date >= weekStart.Date && task.StartAt.Date <= weekEnd.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TaskItem>();
            }
        }

        private void FillTasksGrid(DateTime weekStart)
        {
            var grid = CalendarBorder.Child as Grid;
            if (grid == null) return;
            grid.Children.Clear();

            DateTime weekEnd = weekStart.AddDays(6);
            var tasks = GetTasksForWeek(weekStart);

            
            Dictionary<int, List<int>> dayRowMap = new Dictionary<int, List<int>>();
            for (int i = 0; i < 7; i++)
                dayRowMap[i] = new List<int>();

            foreach (var task in tasks.OrderBy(t => t.StartAt))
            {
                int startOffset = (task.StartAt.Date - weekStart.Date).Days;
                int endOffset = (task.DeadLine.Date - weekStart.Date).Days;

                if (startOffset < 0 || startOffset > 6) continue;
                if (endOffset < 0) continue;

                int column = Math.Max(0, startOffset);
                int columnSpan = Math.Min(6 - column + 1, endOffset - column + 1);

                if (columnSpan <= 0) columnSpan = 1;

                
                int row = 0;
                while (true)
                {
                    bool intersects = false;
                    for (int c = column; c < column + columnSpan; c++)
                    {
                        if (dayRowMap[c].Contains(row))
                        {
                            intersects = true;
                            break;
                        }
                    }

                    if (!intersects) break;
                    row++;
                }

                
                for (int c = column; c < column + columnSpan; c++)
                {
                    dayRowMap[c].Add(row);
                }

               
                var tb = new TextBlock
                {
                    Text = task.Title,
                    Background = Brushes.LightSkyBlue,
                    Margin = new Thickness(2),
                    Padding = new Thickness(4),
                    TextWrapping = TextWrapping.Wrap
                };

                
                var tooltipPanel = new StackPanel();
                tooltipPanel.Children.Add(new TextBlock { Text = $"Заголовок: {task.Title}", FontWeight = FontWeights.Bold });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Описание: {task.Description}", TextWrapping = TextWrapping.Wrap });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Начало: {task.StartAt:G}" });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Дедлайн: {task.DeadLine:G}" });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Статус: {task.Status?.Name ?? "Не указан"}" });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Приоритет: {task.Priority?.Name ?? "Не указан"}" });
                tooltipPanel.Children.Add(new TextBlock { Text = $"Ответственный: {task.AssignedTo?.UserName ?? "Не указан"}" });

                tb.ToolTip = new ToolTip
                {
                    Content = tooltipPanel,
                    Background = Brushes.LightYellow,
                    Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse
                };

                ToolTipService.SetShowDuration(tb, 10000);

                
                if (row >= grid.RowDefinitions.Count)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }

                Grid.SetRow(tb, row);
                Grid.SetColumn(tb, column);
                Grid.SetColumnSpan(tb, columnSpan);
                grid.Children.Add(tb);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            

            var manageTaskControl = new ManageExistingTaskControl(UserDataBase.Users[0]);

            var window = new Window()
            {
                Title = "Редактирование задачи",
                Content = manageTaskControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                Owner = Window.GetWindow(this), 
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();

        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t)
                    yield return t;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private void CalendarUI_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized) return;
            isInitialized = true;

            SetupWeekDays();

            DateTime weekStart = GetStartOfWeek(DateTime.Today);
            FillTasksGrid(weekStart);


            foreach (var btn in FindVisualChildren<Button>(ButtonBorder))
            {
                btn.Click += Btn_Click;
            }
        }

        private void SetupWeekDays()
        {
            DateTime today = DateTime.Today;
            int currentDayIndex = (int)today.DayOfWeek;
            currentDayIndex = currentDayIndex == 0 ? 7 : currentDayIndex; 

            
            var dayGrid = DayBorder.Child as Grid;
            if (dayGrid == null) return;

            for (int col = 0; col < 7; col++)
            {
                DateTime dayDate = today.AddDays(col - currentDayIndex + 1);

                if (GetChildByColumn(dayGrid, col) is DaysUI dayUI)
                {
                    dayUI.SetDateInfo(dayDate);
                }
            }
        }

        private UIElement GetChildByColumn(Grid grid, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetColumn(child) == column)
                    return child;
            }
            return null;
        }
    }
}
