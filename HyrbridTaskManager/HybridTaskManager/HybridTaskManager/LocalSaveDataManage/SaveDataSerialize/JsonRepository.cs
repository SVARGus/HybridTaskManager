using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace HybridTaskManager.LocalSaveDataManage.SaveDataSerialize
{
    public class JsonRepository<T>
    {
        private readonly string _filePath;
        public ObservableCollection<T> Items { get; }

        public JsonRepository(string filePath)
        {
            _filePath = filePath;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<ObservableCollection<T>>(json, GetJsonOptions()) ?? new ObservableCollection<T>();
                Items = new ObservableCollection<T>(data);
            }
            else
            {
                Items = new ObservableCollection<T>();
            }

            Items.CollectionChanged += (_, __) => Save();
            foreach (var item in Items)
            {
                if (item is INotifyPropertyChanged npc)
                {
                    npc.PropertyChanged += (_, __) => Save();
                }
            }
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(Items, GetJsonOptions());
            File.WriteAllText(_filePath, json);
        }

        private JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            };
        }
    }
}
