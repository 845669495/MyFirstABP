﻿using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace MyFirstABP
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MyFirstABPApplicationModule))]
    public class MyFirstABPWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //ABP可以非常轻松地把Application Service的public方法发布成Web Api接口，可以供客户端通过ajax调用。
            //MyFirstABPApplicationModule这个程序集中所有继承了IApplicationService接口的类，都会自动创建相应的ApiController，其中的公开方法，就会转换成WebApi接口方法。
            //可以通过http://xxx/api/services/app/Task/GetTasks这样的路由地址进行调用。
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MyFirstABPApplicationModule).Assembly, "app")
                .Build();
        }
    }
}