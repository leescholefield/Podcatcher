using Podcatcher.ViewModels.Commands;
using Podcatcher.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace Podcatcher.ViewModels
{
    public class NavbarViewModel : BaseViewModel
    {

        #region Properties

        private readonly INavigationService NavService;

        public List<MenuItem> Items { get; set; }

        public ICommand NavigateToViewModelCommand { get; set; }


        #endregion

        #region Initialization

        public  NavbarViewModel() : this(ServiceLocator.Instance.GetService<INavigationService>())
        {
        }

        public NavbarViewModel(INavigationService navService)
        {
            NavService = navService;
            CreateMenuItems();
        }

        private void CreateMenuItems()
        {
            Items = new List<MenuItem>
            {
                new MenuItem
                {
                    DisplayName = "Search",
                    ViewModel = new SearchViewModel()
                },
                new MenuItem
                {
                    DisplayName = "Subscriptions",
                    ViewModel = new PodcastListViewModel()
                }
            };
        }

        protected override void InitializeCommands()
        {
            NavigateToViewModelCommand = new RelayCommand<MenuItem>(NavigateToViewModel_Execute);
        }

        #endregion

        private void NavigateToViewModel_Execute(MenuItem item)
        {
            // PodcastListView expects its Podcast list to be already loaded so we need to pass it to the service locator.
            // FIX THIS: might be better to have a special case in the MainViewModel handling of nav service requests.
            if (item.ViewModel.GetType() == typeof(PodcastListViewModel))
            {
                var pods = ServiceLocator.Instance.GetService<ISubscriptionService>().GetSubscriptions();
                NavService.NavigateTo(item.ViewModel, pods);
                return;
            }
            NavService.NavigateTo(item.ViewModel, item.OptionalParams);
        }

    }

    public class MenuItem
    {
        public BaseViewModel ViewModel { get; set; }

        public string DisplayName { get; set; }

        public object[] OptionalParams { get; set; }
    }
}
