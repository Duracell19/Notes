using MvvmCross.Core.ViewModels;
using Note.Infrastructure;
using Note.Infrastructure.Interfaces;
using Note.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IFileService _fileService;
        private readonly IJsonConverterService _jsonConverter;
        private readonly ISearchNotesService _searchNotesService;
        private readonly ISortNotesService _sortNotesService;
        private MainPageCommands _commands;
        private List<NoteInfo> _notes;
        private List<NoteInfo> _noteInfo;
        private NoteInfo _selectedNote;
        private bool _isNotesVisible;
        /// <summary>
        /// This properties are used to binding MainPageView and MainPageViewModel
        /// </summary>
        public List<NoteInfo> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                RaisePropertyChanged(() => Notes);
            }
        }

        public NoteInfo SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
            }
        }

        public bool IsNotesVisible
        {
            get { return _isNotesVisible; }
            set
            {
                _isNotesVisible = value;
                RaisePropertyChanged(() => IsNotesVisible);
            }
        }
        
        /// <summary>
        /// This propertie is used to work with commands in MainPageViewModel
        /// </summary>
        public MainPageCommands Commands
        {
            get { return _commands; }
        }
        /// <summary>
        /// This is constructor
        /// </summary>
        /// <param name="fileService">This is parameter used to work with file service</param>
        /// <param name="jsonConverter">This is parameter used to work with json converter service</param>
        /// <param name="searchNotesService">This is parameter used to work with search note service</param>
        /// <param name="sortNotesService">This is parameter used to work with sort note service</param>
        public MainPageViewModel(IFileService fileService, 
            IJsonConverterService jsonConverter, 
            ISearchNotesService searchNotesService,
            ISortNotesService sortNotesService)
        {
            _fileService = fileService;
            _jsonConverter = jsonConverter;
            _searchNotesService = searchNotesService;
            _sortNotesService = sortNotesService;

            _commands = new MainPageCommands();

            _commands.ShowInfoCommand = new MvxCommand(() => ShowViewModel<ShowInfoViewModel>());
            _commands.AddNoteCommand = new MvxCommand(AddNote);
            _commands.SortNotesCommand = new MvxCommand(SortNotes);
            _commands.SelectNoteCommand = new MvxCommand<object>(SelectNote);
            _commands.SearchNoteCommand = new MvxCommand<object>(SearchNote);
        }
        /// <summary>
        /// This is asynchronous method to initialize variables
        /// </summary>
        /// <returns>This method return Task</returns>
        public async Task Init()
        {
            _noteInfo = await _fileService.LoadAsync<List<NoteInfo>>(Defines.NOTES_FILE_NAME);
            SetListOfNotes(_noteInfo);
            Notes = _noteInfo;
        }

        public override void Start()
        {
            base.Start();
        }
        /// <summary>
        /// This command is used to add notes
        /// </summary>
        private void AddNote()
        {
            var param = _jsonConverter.Serialize(_noteInfo);
            ShowViewModel<AddNoteViewModel>(new { param });
        }
        /// <summary>
        /// This command is used to sort notes
        /// </summary>
        private void SortNotes()
        {
            Notes = _sortNotesService.SortNotesByNames(_noteInfo);
        }
        /// <summary>
        /// This command is used to view selected note
        /// </summary>
        /// <param name="arg">This parameter is selected note</param>
        private void SelectNote(object arg)
        {
            if (arg is NoteInfo)
            {
                var item = (NoteInfo)arg;
                var param = _jsonConverter.Serialize(item);
                ShowViewModel<ShowNoteViewModel>(new { param });
            }
        }
        /// <summary>
        /// This command is used to search note
        /// </summary>
        /// <param name="arg">This parameter is current text for search</param>
        private void SearchNote(object arg)
        {
            if (arg is string)
            {
                SetListOfNotes(_searchNotesService.FindContainsNotes(_noteInfo, (string)arg));
            }
        }
        /// <summary>
        /// This method is used to show found notes
        /// </summary>
        /// <param name="data">This parameter is list of notes</param>
        private void SetListOfNotes(List<NoteInfo> data)
        {
            if (data == null || data.Count == 0)
            {
                IsNotesVisible = false;
            }
            else
            {
                IsNotesVisible = true;
                Notes = data;
            }
        }
    }
}