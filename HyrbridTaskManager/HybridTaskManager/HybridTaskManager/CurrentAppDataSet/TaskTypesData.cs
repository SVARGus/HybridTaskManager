using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class TaskTypesData
    {
        public static JsonRepository<TaskType> Repository { get; private set; }

        public static ObservableCollection<TaskType> TaskTypes => Repository.Items;

        static TaskTypesData()
        {
            Repository = new JsonRepository<TaskType>("LocalSaveDataManage/Config/TaskDataConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
