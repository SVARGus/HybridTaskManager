﻿namespace HybridTaskManager.DTO.DictionaryEntity
{
    public class TaskType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public TaskType()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }


        public TaskType(string taskTypeName)
        {
            Id = Guid.NewGuid();
            Name = taskTypeName;
        }
    }
}