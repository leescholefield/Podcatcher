using System;

namespace Podcatcher.ViewModels.Services
{
    /// <summary>
    /// Responsible for navigating between different ViewModels.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Registers a function to execute when <see cref="NavigateTo"/> is called.
        /// </summary>
        /// <typeparam name="T">The ViewModel to open</typeparam>
        /// <param name="func">Function that opens the view model.</param>
        void Register<T>(Func<Type, object[], bool> func) where T : BaseViewModel;

        /// <summary>
        /// Executes the function that navigates to <typeparamref name="T"/>. If no function was registered to T this will throw an Exception.
        /// </summary>
        /// <typeparam name="T">ViewModel to navigate to.</typeparam>
        /// <param name="optionalParams">Any addional arguments required for opening the ViewModel.</param>
        void NavigateTo<T>(params object[] optionalParams) where T : BaseViewModel;

        /// <summary>
        /// Executes the function that navigates to <typeparamref name="T"/>. If no function was registered to T this will throw an Exception.
        /// </summary>
        /// <typeparam name="T">ViewModel to navigate to.</typeparam>
        /// <param name="optionalParams">Any addional arguments required for opening the ViewModel.</param>
        void NavigateTo(BaseViewModel viewModel, params object[] optionalParams);

        /// <summary>
        /// Navigates back to the previous ViewModel.
        /// </summary>
        void Previous();
    }
}
