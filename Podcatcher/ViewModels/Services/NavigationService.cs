using System;
using System.Collections.Generic;

namespace Podcatcher.ViewModels.Services
{
    public class NavigationService : INavigationService
    {

        #region Properties

        private readonly Dictionary<Type, Func<Type, object[], bool>> recieverMap;

        private Type previousViewModel;
        private object[] previousViewModelArgs;
        private Type currentViewModel;
        private object[] currentViewModelArgs;

        private static NavigationService instance;
        public static NavigationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NavigationService();
                }

                return instance;

            }
        }

        #endregion

        public NavigationService()
        {
            recieverMap = new Dictionary<Type, Func<Type, object[], bool>>();

        }

        public void NavigateTo<T>(params object[] optionalParams) where T : BaseViewModel
        {
            Type targetType = typeof(T);
            if (!recieverMap.ContainsKey(targetType))
            {
                throw new Exception("No function has been registered for viewmodel of type " + targetType);
            }

            if (currentViewModel != null)
            {
                previousViewModel = currentViewModel;
                previousViewModelArgs = currentViewModelArgs;
            }

            currentViewModel = targetType;
            currentViewModelArgs = optionalParams;

            var f = recieverMap[targetType];
            f(targetType, optionalParams);
        }

        public void NavigateTo(BaseViewModel viewModel, params object[] optionalParams)
        {
            Type targetType = viewModel.GetType();
            if (!recieverMap.ContainsKey(targetType))
            {
                throw new Exception("No function has been registered for viewmodel of type " + targetType);
            }

            if (currentViewModel != null)
            {
                previousViewModel = currentViewModel;
                previousViewModelArgs = currentViewModelArgs;
            }

            currentViewModel = targetType;
            currentViewModelArgs = optionalParams;

            var f = recieverMap[targetType];
            f(targetType, optionalParams);
        }

        public void Register<T>(Func<Type, object[], bool> func) where T : BaseViewModel
        {

            recieverMap.Add(typeof(T), func);
        }

        public void Previous()
        {
            if (previousViewModel == null)
            {
                throw new InvalidOperationException("No previous viewmodel is registered");
            }

            currentViewModel = previousViewModel;
            currentViewModelArgs = previousViewModelArgs;

            var f = recieverMap[previousViewModel];
            f(previousViewModel, previousViewModelArgs);
        }
    }
}
