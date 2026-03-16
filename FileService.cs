using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PersonalBudget
{
    public class FileService
    {
        public static void Save(List<Transaction> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Чтобы JSON был красивым (с переносами)
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var jsonText = JsonSerializer.Serialize(data, options);
            File.WriteAllText(GetFilePath(), jsonText);
        }

        public static List<Transaction> Load() 
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
                return JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(filePath));
            
            return new List<Transaction>();
        }

        private static string GetFilePath()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(folder, "PersonalBudget");
            
            if (!Directory.Exists(appFolder)) 
                Directory.CreateDirectory(appFolder);
            
            return Path.Combine(appFolder, "budget.json");                
        }
    }
}
