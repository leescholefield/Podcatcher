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
