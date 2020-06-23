using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace СS_GO_Launcher.Helpers
{
    public class JsonHelper
    {
        public static async Task<T> Read<T>(string fileName)
        {
            bool isExists = await Task.Run(() => File.Exists(fileName));

            if (!isExists)
            {
                throw new FileNotFoundException($"{fileName} file not exist");
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<T>(fs);
            }
        }

        public static async Task Write<T>(string fileName, T value)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<T>(fs, value);
            }
        }
    }
}
