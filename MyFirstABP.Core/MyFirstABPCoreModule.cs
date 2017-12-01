using System.Reflection;
using Abp.Modules;
using Abp.Zero;
using MyFirstABP.Caching;
using Castle.MicroKernel.Registration;

namespace MyFirstABP
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class MyFirstABPCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(Component.For(typeof(ICacheSyncService<>)).ImplementedBy(typeof(CacheSyncService<>)).LifestyleTransient());
        }
    }
}
