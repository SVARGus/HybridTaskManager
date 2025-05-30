using System.Collections.ObjectModel;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class StatusTypeData
    {
        public static JsonRepository<TaskStatus> Repository { get; private set; }

        public static ObservableCollection<TaskStatus> TaskStatuses => Repository.Items;

        static StatusTypeData()
        {
            Repository = new JsonRepository<TaskStatus>(@"..\..\..\LocalSaveDataManage\Config\StatusTypeConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
