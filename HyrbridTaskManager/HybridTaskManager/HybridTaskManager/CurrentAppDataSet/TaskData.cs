using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskData
    {
        public static JsonRepository<TaskItem> Repository { get; private set; }

        public static ObservableCollection<TaskItem> TaskBase => Repository.Items;

        static TaskData()
        {
            Repository = new JsonRepository<TaskItem>(@"..\..\..\LocalSaveDataManage\Config\TaskDataConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
