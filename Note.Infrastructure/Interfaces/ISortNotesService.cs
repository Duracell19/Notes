using Note.Models;
using System.Collections.Generic;

namespace Note.Infrastructure.Interfaces
{
    public interface ISortNotesService
    {
        List<NoteInfo> SortNotesByNames(IEnumerable<NoteInfo> data);
    }
}
