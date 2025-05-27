using HybridTaskManager.DTO.DictionaryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridTaskManager.UserConrols.CalendarUI
{
    public static class TaskColorHelper
    {
        public static readonly Dictionary<string, string> TaskColorMap = new()
        {
            { "Low", "#A8D5BA" },         // Зелёный
            { "Medium", "#FFD580" },      // Жёлтый
            { "High", "#FF9999" },        // Красный
            { "Critical", "#FF6666" },    // Тёмно-красный
            { "In Progress", "#ADD8E6" }, // Голубой
            { "Done", "#B0E57C" },        // Светло-зелёный
        };

        public static string GetColorFor(TaskItem task)
        {
            if (task == null)
                return "#CCCCCC"; // Цвет по умолчанию

            if (task.Priority != null && TaskColorMap.TryGetValue(task.Priority.Name, out var priorityColor))
                return priorityColor;

            if (task.Status != null && TaskColorMap.TryGetValue(task.Status.Name, out var statusColor))
                return statusColor;

            return "#CCCCCC"; // Цвет по умолчанию
        }
    }
}
