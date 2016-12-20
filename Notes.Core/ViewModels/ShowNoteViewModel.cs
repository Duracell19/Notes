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
        private string _titleOfAttachedFile;

        public ICommand SaveNoteCommand { get; set; }
        public ICommand EditNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        public ICommand AttachFileCommand { get; set; }
        /// <summary>
        /// This properties are used to binding ShowNoteView and ShowNoteViewModel
        /// </summary>
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

        public string TitleOfAttachedFile
        {
            get { return _titleOfAttachedFile; }
            set
            {
                _titleOfAttachedFile = value;
                RaisePropertyChanged(() => TitleOfAttachedFile);
            }
        }
        /// <summary>
        /// This is constructor
        /// </summary>
        /// <param name="fileService">This is parameter used to work with file service</param>
        /// <param name="jsonConverter">This is parameter used to work with json converter service</param>
        /// <param name="changeNoteService">This is parameter used to work with change note service</param>
        public ShowNoteViewModel(IFileService fileService, IJsonConverterService jsonConverter, IChangeNoteService changeNoteService)
        {
            _fileService = fileService;
            _jsonConverter = jsonConverter;
            _changeNoteService = changeNoteService;

            SaveNoteCommand = new MvxCommand(SaveNoteAsync);
            EditNoteCommand = new MvxCommand(EditNote);
            DeleteNoteCommand = new MvxCommand(DeleteNoteAsync);
            AttachFileCommand = new MvxCommand(AttachFile);
        }
        /// <summary>
        /// This is asynchronous method to initialize variables
        /// </summary>
        /// <param name="param">This is parameter which came from MainPageViewModel</param>
        /// <returns>This method return Task</returns>
        public async Task Init(string param)
        {
            _notes = new List<NoteInfo>();
            _noteInfo = _jsonConverter.Deserialize<NoteInfo>(param);

            Title = _noteInfo.Title;
            Text = _noteInfo.Text;
            TitleOfAttachedFile = _noteInfo.TitleOfAttachedFile;

            _notes = await _fileService.LoadAsync<List<NoteInfo>>(Defines.NOTES_FILE_NAME);
        }
        /// <summary>
        /// This command to save notes
        /// </summary>
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
        /// <summary>
        /// This command to edit note
        /// </summary>
        private void EditNote()
        {
            IsNoteEnabled = true;
        }
        /// <summary>
        /// This command to delete note
        /// </summary>
        private async void DeleteNoteAsync()
        {
            await _changeNoteService.DeleteAsync(_notes, _noteInfo);
            ShowViewModel<MainPageViewModel>();
        }
        /// <summary>
        /// This command to attach file
        /// </summary>
        private void AttachFile()
        {

        }
    }
}
