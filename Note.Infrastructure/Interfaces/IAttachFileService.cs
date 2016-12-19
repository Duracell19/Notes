using Plugin.FilePicker.Abstractions;
using System.Threading.Tasks;

namespace Note.Infrastructure.Interfaces
{
    public interface IAttachFileService
    {
        Task<FileData> FilePickerAsync();
    }
}
