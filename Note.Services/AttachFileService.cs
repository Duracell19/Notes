using Note.Infrastructure.Interfaces;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System.Threading.Tasks;

namespace Note.Services
{
    public class AttachFileService : IAttachFileService
    {
        /// <summary>
        /// This is constructor
        /// </summary>
        public AttachFileService()
        {
        }
        /// <summary>
        /// This is asynchronous method is used to open file browser to pick and choose file
        /// </summary>
        /// <returns>This method return Tasks of FileData</returns>
        public async Task<FileData> FilePickerAsync()
        {
            return await CrossFilePicker.Current.PickFile();
        }
    }
}
