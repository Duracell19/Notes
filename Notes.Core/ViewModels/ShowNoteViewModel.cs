using MvvmCross.Core.ViewModels;
using Note.Infrastructure;
using Note.Infrastructure.Interfaces;
using Note.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.Core.ViewModels
{
    public class ShowNoteViewModel : MvxViewModel
    {
        private readonly IFileService _fileService;
        private readonly IJsonConverterService _jsonConverter;
        private readonly IChangeNoteService _changeNoteService;
        private List<NoteInfo> _notes;
        private NoteInfo _noteInfo;
        private string _title;
        private string _text;
        private bool _isNoteEnabled;

        public ICommand SaveNoteCommand { get; set; }
        public ICommand EditNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public bool IsNoteEnabled
        {
            get { return _isNoteEnabled; }
            set
            {
                _isNoteEnabled = value;
                RaisePropertyChanged(() => IsNoteEnabled);
            }
        }

        public ShowNoteViewModel(IFileService fileService,
            IJsonConverterService jsonConverter,
            IChangeNoteService changeNoteService)
        {
            _fileService = fileService;
            _jsonConverter = jsonConverter;
            _changeNoteService = changeNoteService;

            SaveNoteCommand = new MvxCommand(SaveNoteAsync);
            EditNoteCommand = new MvxCommand(EditNote);
            DeleteNoteCommand = new MvxCommand(DeleteNoteAsync);
        }

        public async Task Init(string param)
        {
            _notes = new List<NoteInfo>();
            _noteInfo = _jsonConverter.Deserialize<NoteInfo>(param);

            Title = _noteInfo.Title;
            Text = _noteInfo.Text;

            _notes = await _fileService.LoadAsync<List<NoteInfo>>(Defines.NOTES_FILE_NAME);
        }

        private async void SaveNoteAsync()
        {
            var item = new NoteInfo();
            item.DateOfCreation = _noteInfo.DateOfCreation;
            item.Title = Title;
            item.Text = Text;
            item.DateOfLastChange = string.Format("Date of last change: {0}", DateTime.Now.ToString());
            await _changeNoteService.SaveAsync(_notes, item);
            ShowViewModel<MainPageViewModel>();
        }

        private void EditNote()
        {
            IsNoteEnabled = true;
        }

        private async void DeleteNoteAsync()
        {
            await _changeNoteService.DeleteAsync(_notes, _noteInfo);
            ShowViewModel<MainPageViewModel>();
        }
    }
}
