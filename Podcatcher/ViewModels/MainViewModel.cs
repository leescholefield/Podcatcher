using Podcatcher.Models;
using Podcatcher.ViewModels.Services;

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
            RegisterNavigationCommands();

            // just for testing
            MainView = new PodcastListViewModel
            {
               Podcasts = new System.Collections.Generic.List<Models.Podcast>
               {
                   new Models.Podcast
                   {
                       Title = "Revolutions",
                       Author = "Mike Duncan",
                       ImageUrl = "https://ssl-static.libsyn.com/p/assets/3/4/5/f/345fbd6a253649c0/RevolutionsLogo_V2.jpg"
                   },
                   new Models.Podcast
                   {
                       Title = "Risky Business",
                       Author = "Risky.Biz",
                       ImageUrl = "https://risky.biz/static/img/rbipod2.jpg"
                   }
               }
            };
        }

        private void RegisterNavigationCommands()
        {
            var navService = ServiceLocator.Instance.GetService<INavigationService>();
            navService.Register<PodcastViewModel>( (t, args) =>
            {
                MainView = new PodcastViewModel() { Podcast = (Podcast)args[0] };
                return true;
            });
        }
    }
}
