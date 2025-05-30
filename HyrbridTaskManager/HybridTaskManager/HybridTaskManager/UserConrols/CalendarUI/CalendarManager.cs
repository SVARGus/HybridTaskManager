using HybridTaskManager.DTO.DictionaryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HybridTaskManager.UserConrols.CalendarUI
{
    /// <summary>
    /// Собирает и готовит к визуализации задачи на неделю,
    /// рассчитывает позиционирование по строкам/колонкам.
    /// </summary>
    public class CalendarManager
    {
        private readonly Func<DateTime, List<TaskItem>> _getTasksForWeek;
        private readonly Func<TaskItem, string> _pickColorForBackground;

        public CalendarManager(
            Func<DateTime, List<TaskItem>> getTasksForWeek,
            Func<TaskItem, string> pickColorForBackground)
        {
            _getTasksForWeek = getTasksForWeek;
            _pickColorForBackground = pickColorForBackground;
        }

        /// <summary>
        /// Рассчитывает, в какой строке должна размещаться каждая задача,
        /// и возвращает готовые контролы для вставки в грид.
        /// </summary>
        public IEnumerable<(TaskItemControl Control, int Row, int StartCol, int ColSpan)>
            BuildTaskControls(DateTime weekStart)
        {
            var tasks = _getTasksForWeek(weekStart);
            var placed = new List<(int startCol, int endCol, int row)>();

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];

                // обрезаем по границам недели
                var (sc, ec) = ClampToVisibleWeek(weekStart, task.StartAt.Date, task.DeadLine.Date);
                if (sc == null) continue;

                // ищем строку, где нет пересечений
                int row = FindAvailableRow(placed, sc.Value, ec.Value);
                placed.Add((sc.Value, ec.Value, row));

                // создаём контрол
                var ctl = new TaskItemControl
                {
                    Title = task.Title,
                    BackgroundColor =
                        (SolidColorBrush)(new BrushConverter().ConvertFromString(
                            _pickColorForBackground(task))!),
                    PriorityColor =
                        (SolidColorBrush)(new BrushConverter().ConvertFromString(
                            task.Priority.HexColorCode)!)
                };

                yield return (ctl, row, sc.Value, ec.Value - sc.Value + 1);
            }
        }

        private (int? startCol, int? endCol) ClampToVisibleWeek(
            DateTime weekStart, DateTime taskStart, DateTime taskEnd)
        {
            var weekEnd = weekStart.AddDays(6);
            if (taskEnd < weekStart || taskStart > weekEnd)
                return (null, null);
            int sc = Math.Max(0, (taskStart - weekStart).Days);
            int ec = Math.Min(6, (taskEnd - weekStart).Days);
            return (sc, ec);
        }

        private int FindAvailableRow(
            List<(int startCol, int endCol, int row)> placed,
            int start, int end)
        {
            for (int r = 0; r < 8; r++)
            {
                bool collision = placed.Any(p =>
                    p.row == r && !(p.endCol < start || p.startCol > end));
                if (!collision) return r;
            }
            return 7;
        }
    }
}
