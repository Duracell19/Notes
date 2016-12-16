using System.Threading.Tasks;

namespace Note.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<T> LoadAsync<T>(string fileName);
        Task SaveAsync(string fileName,  object obj);
    }
}
