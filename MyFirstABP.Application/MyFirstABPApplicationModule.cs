using System.Reflection;
using Abp.Modules;
using AutoMapper;
using System;
using Abp.AutoMapper;
using Abp.Runtime.Caching;

namespace MyFirstABP
{
    [DependsOn(typeof(MyFirstABPCoreModule), typeof(AbpAutoMapperModule))]
    public class MyFirstABPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyAuthorizationProvider>();

            base.PreInitialize();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
