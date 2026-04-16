using BasinTakip.Core.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Dependecy
{
    public class BasicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(IocManager.AssemblyDirectory, mask: "BasinTakip.*"))
                    .BasedOn<ITransientDependecy>()
                    .WithServiceDefaultInterfaces()
                    .Configure(p => p.LifestyleTransient()
                                    .Interceptors<InterceptAll>()));

            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(IocManager.AssemblyDirectory, mask: "BasinTakip.*"))
                    .BasedOn<ISingletonDependency>()
                    .WithServiceDefaultInterfaces()
                    .Configure(p => p.LifestyleSingleton()
                                    .Interceptors<InterceptAll>()));

            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(IocManager.AssemblyDirectory, mask: "BasinTakip.*"))
                    .BasedOn<IScoppedDependency>()
                    .WithServiceDefaultInterfaces()
                    .Configure(p => p.LifestyleScoped()
                                    .Interceptors<InterceptAll>()));
        }
    }
}
