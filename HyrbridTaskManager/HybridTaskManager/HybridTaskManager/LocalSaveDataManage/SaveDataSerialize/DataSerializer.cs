using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HybridTaskManager.LocalSaveDataManage.SaveDataSerialize
{
    public static class DataSerializer<T> 
    {
        public static void SaveToJson(string filePath, ObservableCollection<T> collection)
        {
            var json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static ObservableCollection<T> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
        }
    }
}
