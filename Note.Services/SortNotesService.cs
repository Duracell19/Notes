using Note.Infrastructure.Interfaces;
using Note.Models;
using System.Collections.Generic;
using System.Linq;

namespace Note.Services
{
    public class SortNotesService : ISortNotesService
    {
        /// <summary>
        /// This is method to sort notes by name
        /// </summary>
        /// <param name="data">This parameter is list of data</param>
        /// <returns>This method return sorted data</returns>
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
