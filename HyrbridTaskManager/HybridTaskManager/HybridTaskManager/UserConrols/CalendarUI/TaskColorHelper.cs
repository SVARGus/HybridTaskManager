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
        private static readonly Dictionary<string, string> TaskColorMap = new()
    {
        { "Low", "#A8D5BA" },
        { "Medium", "#FFD580" },
        { "High", "#FF9999" },
        { "Critical", "#FF6666" },
        { "In Progress", "#ADD8E6" },
        { "Done", "#B0E57C" }
    };

        public static string GetColorForTask(TaskItem task)
        {
            if (task.Priority?.Name != null && TaskColorMap.TryGetValue(task.Priority.Name, out var color))
                return color;

            if (task.Status?.Name != null && TaskColorMap.TryGetValue(task.Status.Name, out color))
                return color;

            return "#D3D3D3"; // Default — серый
        }
    }
}
