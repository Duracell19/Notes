using MvvmCross.Core.ViewModels;
using Note.Infrastructure;

namespace Notes.Core.ViewModels
{
    public class ShowInfoViewModel : MvxViewModel
    {
        public ShowInfoViewModel()
        {
        }

        public string TextAbout
        {
            get { return Decription.ABOUT_INFORMATION; }
        }
    }
}
