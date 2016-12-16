using Note.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Infrastructure.Interfaces
{
    public interface IChangeNoteService
    {
        Task SaveAsync(List<NoteInfo> notes, NoteInfo saveItem);
        Task DeleteAsync(List<NoteInfo> notes, NoteInfo deleteItem);
    }
}
