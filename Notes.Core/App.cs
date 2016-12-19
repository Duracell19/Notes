using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Note.Infrastructure.Interfaces;
using Note.Services;
using Notes.Core.ViewModels;

namespace Notes.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<IChangeNoteService, ChangeNoteService>();
            Mvx.RegisterType<IFileService, FileService>();
            Mvx.RegisterType<IJsonConverterService, JsonConverterService>();
            Mvx.RegisterType<ISearchNotesService, SearchNotesService>();
            Mvx.RegisterType<ISortNotesService, SortNotesService>();
            Mvx.RegisterType<IAttachFileService, AttachFileService>();
            

            RegisterAppStart<MainPageViewModel>();
        }
    }
}
