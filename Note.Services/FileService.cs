using Note.Infrastructure.Interfaces;
using PCLStorage;
using System.Threading.Tasks;

namespace Note.Services
{
    public class FileService : IFileService
    {
        private readonly IJsonConverterService _jsonConverter;

        public FileService(IJsonConverterService jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public async Task<T> LoadAsync<T>(string fileName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            return _jsonConverter.Deserialize<T>(ReadFileAsync(file, fileName).Result);
        }

        public async Task SaveAsync(string fileName, object obj)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await file.WriteAllTextAsync(_jsonConverter.Serialize(obj));
        }

        public async Task<string> ReadFileAsync(IFile file, string fileName)
        {
            return await Task.Run(() => file.ReadAllTextAsync()).ConfigureAwait(false);
        }
    }
}
