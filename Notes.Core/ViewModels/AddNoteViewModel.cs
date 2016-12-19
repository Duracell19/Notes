using MvvmCross.Core.ViewModels;
using Note.Infrastructure;
using Note.Infrastructure.Interfaces;
using Note.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Notes.Core.ViewModels
{
    public class AddNoteViewModel : MvxViewModel
    {
        private readonly IFileService _fileService;
        private readonly IJsonConverterService _jsonConverter;
        private readonly IAttachFileService _attachFileService;
        private NoteInfo _noteInfo;
        private List<NoteInfo> _notes;
        private string _title;
        private string _text;

        public ICommand SaveNoteCommand { get; set; }
        public ICommand AttachFileCommand { get; set; }
        /// <summary>
        /// This properties are used to binding AddNoteView and AddNoteViewModel.
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
        /// <summary>
        /// This is constructor
        /// </summary>
        /// <param name="fileService">This is parameter used to work with file service</param>
        /// <param name="jsonConverter">This is parameter used to work with json converter service</param>
        /// <param name="attachFileService">This is parameter used to work with attach file service</param>
        public AddNoteViewModel(IFileService fileService, IJsonConverterService jsonConverter, IAttachFileService attachFileService)
        {
            _fileService = fileService;
            _jsonConverter = jsonConverter;
            _attachFileService = attachFileService;

            SaveNoteCommand = new MvxCommand(SaveNoteAsync);
            AttachFileCommand = new MvxCommand(AttachFileAsync);
        }
        /// <summary>
        /// This is method to initialize variables
        /// </summary>
        /// <param name="param">This is parameter which came from MainPageViewModel</param>
        public void Init(string param)
        {
            _noteInfo = new NoteInfo();
            _notes = _jsonConverter.Deserialize<List<NoteInfo>>(param);
            if (_notes == null)
            {
                _notes = new List<NoteInfo>();
            }
        }
        /// <summary>
        /// This command is used to save notes
        /// </summary>
        private async void SaveNoteAsync()
        {
            _noteInfo.Text = Text;
            _noteInfo.Title = Title;
            _noteInfo.DateOfCreation = string.Format("Date of creation: {0}", DateTime.Now.ToString());
            _noteInfo.DateOfLastChange = "Date of last change: The note hasn't been modified";
            _notes.Add(_noteInfo);
            await _fileService.SaveAsync(Defines.NOTES_FILE_NAME, _notes);
            ShowViewModel<MainPageViewModel>();
        }
        /// <summary>
        /// This command is used to attach file
        /// </summary>
        private async void AttachFileAsync()
        {
            
        }
    }
}