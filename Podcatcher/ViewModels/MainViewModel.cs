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
                    ImageUrl = "http://static.libsyn.com/p/assets/3/4/5/f/345fbd6a253649c0/RevolutionsLogo_V2.jpg",
                    Episodes = new System.Collections.Generic.List<Models.Episode>
                    {
                        new Models.Episode
                        {
                            Title = "FIRST",
                            Author = "Mike Duncan",
                            Description = "asda  sd a sd gsd f s fdasd as as d asd  asd a sda sd asd sd f dfg fgh fh ghfg"
                        },
                        new Models.Episode
                        {
                            Title = "Second",
                            Author = "Mike Duncan",
                            Description = "asdas  sdfg a sd a asd as a sd asd a sd as da sd as das d as dsa d a"
                        }
                    }

                }
            };
        }
    }
}
