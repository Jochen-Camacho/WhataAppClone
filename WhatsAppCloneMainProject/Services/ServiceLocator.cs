using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}