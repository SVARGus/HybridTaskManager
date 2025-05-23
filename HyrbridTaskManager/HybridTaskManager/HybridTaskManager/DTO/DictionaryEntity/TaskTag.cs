using System.Collections.ObjectModel;

namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskTag
    {
        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public List<Guid> TagsId { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }

        public TaskTag() { }

        public TaskTag(TaskItem taskItem, ObservableCollection<Tag> tags)
        {
            TaskItem = taskItem;
            TaskItemId = TaskItem.Id;
            Tags = tags;
        }
    }

}