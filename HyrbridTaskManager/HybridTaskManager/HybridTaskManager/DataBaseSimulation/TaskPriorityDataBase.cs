using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.DictionaryEntity;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskPriorityDataBase
    {
        public static ObservableCollection<TaskPriority> TaskPriorities = new ObservableCollection<TaskPriority>
        {
            new TaskPriority("Низкий",0,"#50ca3a"),
            new TaskPriority("Средний",1,"#b9ca3a"),
            new TaskPriority("Высокий",2,"#ca893a"),
            new TaskPriority("Срочный",3,"#ca3a3a"),
        };
    }
}
