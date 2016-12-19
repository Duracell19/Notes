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
        /// <summary>
        /// This is constructor
        /// </summary>
        /// <param name="fileService">This is parameter used to work with file service></param>
        public ChangeNoteService(IFileService fileService)
        {
            _fileService = fileService;
        }
        /// <summary>
        /// This is asynchronous method is used to save notes
        /// </summary>
        /// <param name="notes">This parameter is list of notes</param>
        /// <param name="saveItem">This parameter is current note</param>
        /// <returns>This method return Task</returns>
        public async Task SaveAsync(List<NoteInfo> notes, NoteInfo saveItem)
        {
            var index = IndexOfNote(notes, saveItem);
            if (index != -1)
            {
                notes[index] = saveItem;
                await _fileService.SaveAsync(Defines.NOTES_FILE_NAME, notes);
            }
        }
        /// <summary>
        /// This is asynchronous method is used to delete note
        /// </summary>
        /// <param name="notes">This parameter is list of notes</param>
        /// <param name="deleteItem">This parameter is current note</param>
        /// <returns>This method return Task</returns>
        public async Task DeleteAsync(List<NoteInfo> notes, NoteInfo deleteItem)
        {
            var index = IndexOfNote(notes, deleteItem);
            if (index != -1)
            {
                notes.RemoveAt(index);
                await _fileService.SaveAsync(Defines.NOTES_FILE_NAME, notes);
            }
        }
        /// <summary>
        /// This is method is used to find index of selected item
        /// </summary>
        /// <param name="notes">This parameter is list of notes</param>
        /// <param name="item">>This parameter is current item</param>
        /// <returns>This method return index</returns>
        private int IndexOfNote(List<NoteInfo> notes, NoteInfo item)
        {
            return notes.FindIndex(0, notes.Count, note => note.DateOfCreation.Equals(item.DateOfCreation));
        }
    }
}
