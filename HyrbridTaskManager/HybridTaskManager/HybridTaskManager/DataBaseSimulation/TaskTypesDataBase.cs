using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskTypesDataBase
    {
        public static ObservableCollection<TaskType> TaskTypes { get; set; } = new ObservableCollection<TaskType>
        {
            new TaskType("Рабочая задача"),
            new TaskType("Глобальная задача"),
        };
    }
}
