using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject;

namespace BusinessLogic.Infrastructure
{
    public static class DependencyResolver
    {
        private static IKernel _kernel;

        public static void Initialize(IKernel kernel)
        {
            _kernel = kernel;
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
