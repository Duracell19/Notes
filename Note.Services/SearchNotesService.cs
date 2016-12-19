using Note.Infrastructure.Interfaces;
using Note.Models;
using System.Collections.Generic;
using System.Linq;

namespace Note.Services
{
    public class SearchNotesService : ISearchNotesService
    {
        /// <summary>
        /// This is method is used to search a notes
        /// </summary>
        /// <param name="data">This parameter is list of notes</param>
        /// <param name="findText">This parameter is text to search</param>
        /// <returns>This method return list of found notes</returns>
        public List<NoteInfo> FindContainsNotes(IEnumerable<NoteInfo> data, string findText)
        {
            if (data == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(findText))
            {
                return data.ToList();
            }
            var result = data.Where(x => x.Title.ToLower().Contains(findText.ToLower())).DefaultIfEmpty().ToList();
            return (result.Count == 1 && result[0] == null) ? null : result;
        }
    }
}