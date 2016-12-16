using Note.Infrastructure;
using Note.Infrastructure.Interfaces;
using Note.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Services
{
    public class ChangeNoteService : IChangeNoteService
    {
        private readonly IFileService _fileService;

        public ChangeNoteService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task SaveAsync(List<NoteInfo> notes, NoteInfo saveItem)
        {
            var index = IndexOfNote(notes, saveItem);
            if (index != -1)
            {
                notes[index] = saveItem;
                await _fileService.SaveAsync(Defines.NOTES_FILE_NAME, notes);
            }
        }

        public async Task DeleteAsync(List<NoteInfo> notes, NoteInfo deleteItem)
        {
            var index = IndexOfNote(notes, deleteItem);
            if (index != -1)
            {
                notes.RemoveAt(index);
                await _fileService.SaveAsync(Defines.NOTES_FILE_NAME, notes);
            }
        }

        private int IndexOfNote(List<NoteInfo> notes, NoteInfo item)
        {
            return notes.FindIndex(0, notes.Count, note => note.DateOfCreation.Equals(item.DateOfCreation));
        }
    }
}
