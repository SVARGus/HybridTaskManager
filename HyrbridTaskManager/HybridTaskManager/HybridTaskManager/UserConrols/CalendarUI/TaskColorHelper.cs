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
        private static readonly List<string> Colors = new()
    {
        "#A8D5BA", "#FFD580", "#FF9999", "#ADD8E6",
        "#B0E57C", "#FFB6C1", "#DDA0DD", "#87CEFA"
    };

        public static string GetColorByIndex(int index)
        {
            return Colors[index % Colors.Count];
        }
    }
}
