using System.Windows.Input;

namespace Note.Models
{
    /// <summary>
    /// This class is used to define a model of MainPageVieModel's commands
    /// </summary>
    public class MainPageCommands
    {
        public ICommand AddNoteCommand { get; set; }
        public ICommand SearchNoteCommand { get; set; }
        public ICommand SortNotesCommand { get; set; }
        public ICommand ShowInfoCommand { get; set; }
        public ICommand SelectNoteCommand { get; set; }
    }
}
