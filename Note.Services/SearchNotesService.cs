using Note.Infrastructure.Interfaces;
using Note.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Note.Services
{
    public class SearchNotesService : ISearchNotesService
    {
        public List<NoteInfo> FindEqualsNotes(IEnumerable<NoteInfo> data, string findText)
        {
            if (data == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(findText))
            {
                return data.ToList();
            }
            var result = data.Where(x => x.Title.Equals(findText, StringComparison.CurrentCultureIgnoreCase)).DefaultIfEmpty().ToList();
            return (result.Count == 1 && result[0] == null) ? null : result;
        }

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