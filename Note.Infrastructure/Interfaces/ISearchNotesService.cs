using Note.Models;
using System.Collections.Generic;

namespace Note.Infrastructure.Interfaces
{
    public interface ISearchNotesService
    {
        List<NoteInfo> FindEqualsNotes(IEnumerable<NoteInfo> data, string findText);
        List<NoteInfo> FindContainsNotes(IEnumerable<NoteInfo> data, string findText);
    }
}
