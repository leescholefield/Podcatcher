namespace Podcatcher.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private BaseViewModel _mainView;
        public BaseViewModel MainView
        {
            get
            {
                return _mainView;
            }
            set
            {
                _mainView = value;
                OnPropertyChanged("MainView");
            }
        }

        public MainViewModel()
        {
            MainView = new PodcastViewModel
            {
                Podcast = new Models.Podcast
                {
                    Title = "Revolutions",
                    Author = "Mike Duncan",
                    ImageUrl = "http://static.libsyn.com/p/assets/3/4/5/f/345fbd6a253649c0/RevolutionsLogo_V2.jpg"

                }
            };
        }
    }
}
