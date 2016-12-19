namespace Note.Models
{
    /// <summary>
    /// This class is used to define a model of notes
    /// </summary>
    public class NoteInfo
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string DateOfCreation { get; set; }
        public string DateOfLastChange { get; set; }
    }
}
