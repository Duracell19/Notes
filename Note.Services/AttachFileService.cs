using Note.Infrastructure.Interfaces;
using Plugin.FilePicker;
using System.Threading.Tasks;

namespace Note.Services
{
    public class AttachFileService : IAttachFileService
    {
        public AttachFileService()
        {

        }

        public async Task FilePickerAsync()
        {
            var result = await CrossFilePicker.Current.PickFile();
            var data = result.DataArray;
            var title = result.FileName;

        }
    }
}
