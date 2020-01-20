﻿using Podcatcher.Models;
using Podcatcher.Models.Database;
using Podcatcher.Models.Net;
using Podcatcher.ViewModels.Services;
using System;
using System.Collections.Generic;

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

        private BaseViewModel _navBar;
        public BaseViewModel NavBar
        {
            get
            {
                return _navBar;
            }
            set
            {
                _navBar = value;
                OnPropertyChanged("NavBar");
            }
        }

        public MainViewModel()
        {
            RegisterNavigationCommands();
            MainView = new PodcastListViewModel();
            NavBar = new NavbarViewModel();
        }

        private void RegisterNavigationCommands()
        {
            var navService = ServiceLocator.Instance.GetService<INavigationService>();

            navService.Register<PodcastViewModel>( (t, args) =>
            {
                MainView = new PodcastViewModel() { Podcast = (Podcast)args[0] };
                return true;
            });

            navService.Register<PodcastListViewModel>((t, args) =>
           {
               MainView = new PodcastListViewModel() { Podcasts = (List<Podcast>)args[0] };
               return true;
           });

            navService.Register<SearchViewModel>((t, args) =>
           {
               MainView = new SearchViewModel();
               return true;
           });
        }
    }
}
