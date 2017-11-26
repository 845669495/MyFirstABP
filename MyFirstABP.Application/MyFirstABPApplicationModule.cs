using System.Reflection;
using Abp.Modules;
using MyFirstABP.DTO;
using AutoMapper;
using System;
using Abp.AutoMapper;

namespace MyFirstABP
{
    [DependsOn(typeof(MyFirstABPCoreModule))]
    public class MyFirstABPApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //初始化AutoMapper
            Mapper.Initialize(configuration =>
            {
                configuration.CreateAutoAttributeMaps(typeof(TaskDto));
            });
        }
    }

    internal static class AutoMapperConfigurationExtensions
    {
        public static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
    }
}
