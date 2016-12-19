using System.Threading.Tasks;

namespace Note.Infrastructure.Interfaces
{
    public interface IAttachFileService
    {
        Task FilePickerAsync();
    }
}
