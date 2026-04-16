using BasinTakip.Core.Dependecy;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;

namespace System
{
    public static class IocManager
    {
        static WindsorContainer container = new WindsorContainer();

        static public string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;

                var uri = new UriBuilder(codeBase);

                var path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }
        public static void Install()
        {
            Install(new BasicInstaller());

            container.Register(Component.For<InterceptAll>()
                                   .LifestyleTransient());
        }
        public static void Install(object installer)
        {
            container.Install(installer as IWindsorInstaller);
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public static void Dispose()
        {
            container.Dispose();
        }

        public static T[] ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        public static void Release(object instance)
        {
            container.Release(instance);
        }

        public static IDisposable BeginScope()
        {
            return container.BeginScope();
        }
    }
}
