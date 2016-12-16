using Note.Infrastructure.Interfaces;
using Note.Models;
using System.Collections.Generic;
using System.Linq;

namespace Note.Services
{
    public class SortNotesService : ISortNotesService
    {
        public List<NoteInfo> SortNotesByNames(IEnumerable<NoteInfo> data)
        {
            if (data == null)
            {
                return null;
            }
            return data.OrderBy(x => x.Title).ToList();
        }
    }
}
