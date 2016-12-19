using Note.Infrastructure.Interfaces;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System.Threading.Tasks;

namespace Note.Services
{
    public class AttachFileService : IAttachFileService
    {
        public AttachFileService()
        {
        }

        public async Task<FileData> FilePickerAsync()
        {
            var result = await CrossFilePicker.Current.PickFile();
            return result;
        }
    }
}
