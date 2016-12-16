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

        public MainPageCommands Commands
        {
            get { return _commands; }
        }

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

        private void AddNote()
        {
            var param = _jsonConverter.Serialize(_noteInfo);
            ShowViewModel<AddNoteViewModel>(new { param });
        }

        private void SortNotes()
        {
            Notes = _sortNotesService.SortNotesByNames(_noteInfo);
        }

        private void SelectNote(object arg)
        {
            if (arg is NoteInfo)
            {
                var item = (NoteInfo)arg;
                var param = _jsonConverter.Serialize(item);
                ShowViewModel<ShowNoteViewModel>(new { param });
            }
        }

        private void SearchNote(object arg)
        {
            if (arg is string)
            {
                SetListOfNotes(_searchNotesService.FindContainsNotes(_noteInfo, (string)arg));
            }
        }

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
