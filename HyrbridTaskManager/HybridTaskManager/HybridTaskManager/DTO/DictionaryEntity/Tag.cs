namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Tag(string tagName)
        {
            Id = Guid.NewGuid();
            Name = tagName;
        }
    }
}