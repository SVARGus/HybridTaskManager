using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskPriorityData
    {
        public static JsonRepository<TaskPriority> Repository { get; private set; }

        public static ObservableCollection<TaskPriority> TaskPriorities => Repository.Items;

        static TaskPriorityData()
        {
            Repository = new JsonRepository<TaskPriority>(@"..\..\..\LocalSaveDataManage\Config\TaskPriorityConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
