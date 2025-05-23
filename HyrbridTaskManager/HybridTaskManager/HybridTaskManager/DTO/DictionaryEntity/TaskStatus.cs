namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; } // для сортировки: Новая = 1, В работе = 2 и т.д.
        public string HexColorCode { get; set; }

        public TaskStatus() { }

        public TaskStatus(string statusName, int order, string colorCode)
        {
            Id = Guid.NewGuid();
            Name = statusName;
            Order = order;
            HexColorCode = colorCode;
        }
    }

}