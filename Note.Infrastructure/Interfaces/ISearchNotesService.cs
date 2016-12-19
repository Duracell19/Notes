using Note.Models;
using System.Collections.Generic;

namespace Note.Infrastructure.Interfaces
{
    public interface ISearchNotesService
    {
        List<NoteInfo> FindContainsNotes(IEnumerable<NoteInfo> data, string findText);
    }
}
