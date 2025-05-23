namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskPriority
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; } // можно использовать для сортировки по срочности
        public string HexColorCode { get; set; }

        public TaskPriority()
        {
            
        }

        public TaskPriority(string priorityName, int priorityLevel, string colorCode)
        {
            Id = Guid.NewGuid();
            Name = priorityName;
            Level = priorityLevel;
            HexColorCode = colorCode;
        }
    }
}