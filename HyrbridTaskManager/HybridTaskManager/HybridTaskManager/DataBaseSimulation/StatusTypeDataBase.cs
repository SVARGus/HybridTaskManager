using System.Collections.ObjectModel;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class StatusTypeDataBase
    {
        public static ObservableCollection<DTO.DictionaryEntity.TaskStatus> TaskStatuses { get; set; } = new ObservableCollection<DTO.DictionaryEntity.TaskStatus>
        {
            new DTO.DictionaryEntity.TaskStatus("Назначено",0,"#af33ff"),
            new DTO.DictionaryEntity.TaskStatus("В процессе",1,"#fff033"),
            new DTO.DictionaryEntity.TaskStatus("Выполнено",2,"#5bff33"),
            new DTO.DictionaryEntity.TaskStatus("Просрочено",3, "#ca3a3a")
        };
    }
}
