using Note.Infrastructure.Interfaces;
using PCLStorage;
using System.Threading.Tasks;

namespace Note.Services
{
    public class FileService : IFileService
    {
        private readonly IJsonConverterService _jsonConverter;
        /// <summary>
        /// This is constructor
        /// </summary>
        /// <param name="jsonConverter">This is parameter used to work with json converter service</param>
        public FileService(IJsonConverterService jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }
        /// <summary>
        /// This is asynchronous method is used to load data from file
        /// </summary>
        /// <typeparam name="T">This parameter is type of loaded data</typeparam>
        /// <param name="fileName">This parameter is name of file</param>
        /// <returns>This method return Task<typeparamref name=">"/>This parameter is type of loaded data</returns>
        public async Task<T> LoadAsync<T>(string fileName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            return _jsonConverter.Deserialize<T>(ReadFileAsync(file, fileName).Result);
        }
        /// <summary>
        /// This is asynchronous method is used to save data to file
        /// </summary>
        /// <param name="fileName">This parameter is name of file</param>
        /// <param name="obj">This parameter is obj to save</param>
        /// <returns>This method return Task</returns>
        public async Task SaveAsync(string fileName, object obj)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await file.WriteAllTextAsync(_jsonConverter.Serialize(obj));
        }
        /// <summary>
        ///  This is asynchronous method is used to read file
        /// </summary>
        /// <param name="file">This parameter is object of file</param>
        /// <param name="fileName">This parameter is name of file</param>
        /// <returns>This method return Tasks of strings </returns>
        public async Task<string> ReadFileAsync(IFile file, string fileName)
        {
            return await Task.Run(() => file.ReadAllTextAsync()).ConfigureAwait(false);
        }
    }
}
