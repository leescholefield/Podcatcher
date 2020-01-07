using System;
using System.Collections.Generic;

namespace Podcatcher.ViewModels.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<object, object> registeredServices;

        private static ServiceLocator _instance;

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to prevent more than one instance of this class being created.
        /// </summary>
        private ServiceLocator()
        {
            registeredServices = new Dictionary<object, object>
            {
                
            };

        }

        /// <summary>
        /// Returns an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of service to return. Note, this will be the interface type, not the implementation type.</typeparam>
        /// <returns>An instance of type T.</returns>
        public T GetService<T>()
        {
            try
            {
                return (T)registeredServices[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service has not been registered. Service name = " + typeof(T));
            }
        }
    }
}
}
