using System.Reflection;
using Abp.Modules;

namespace MyFirstABP
{
    public class MyFirstABPCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
