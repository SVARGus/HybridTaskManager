using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class UserData
    {
        public static JsonRepository<User> Repository { get; private set; }

        public static ObservableCollection<User> Users => Repository.Items;

        static UserData()
        {
            Repository = new JsonRepository<User>("LocalSaveDataManage/Config/TaskDataConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
