using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using MyFirstABP.EntityFramework;
using Abp.Zero.EntityFramework;

namespace MyFirstABP
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(MyFirstABPCoreModule))]
    public class MyFirstABPDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<MyFirstABPDbContext>(null);
        }
    }
}
