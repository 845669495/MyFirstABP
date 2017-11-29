using System.Reflection;
using Abp.Modules;
using Abp.Zero;

namespace MyFirstABP
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class MyFirstABPCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
