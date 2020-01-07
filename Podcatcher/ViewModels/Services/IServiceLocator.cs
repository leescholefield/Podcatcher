namespace Podcatcher.ViewModels.Services
{
    public interface IServiceLocator
    {

        /// <summary>
        /// Returns an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Service to return.</typeparam>
        /// <returns>An instance of <typeparamref name="T"/>, if it has been registered.</returns>
        T GetService<T>();
    }
}
