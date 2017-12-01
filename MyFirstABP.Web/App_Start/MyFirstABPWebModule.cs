using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Configuration.Startup;
using Abp.Runtime.Caching.Redis;
using Abp.Runtime.Caching;

namespace MyFirstABP.Web
{
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(MyFirstABPDataModule), 
        typeof(MyFirstABPApplicationModule),
        typeof(AbpRedisCacheModule),
        typeof(MyFirstABPWebApiModule))]
    public class MyFirstABPWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //配置使用Redis缓存
            Configuration.Caching.UseRedis();
            //IocManager.Register<ICacheManager, AbpRedisCacheManager>();
            //如果Redis在本机,并且使用的默认端口,下面的代码可以不要
            //Configuration.Modules.AbpRedisCacheModule().ConnectionStringKey = "KeyName";


            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));
            Configuration.Localization.Languages.Add(new LanguageInfo("ja", "日本語", "famfamfam-flag-jp"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    MyFirstABPConsts.LocalizationSourceName,
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Localization/MyFirstABP")
                        )
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<MyFirstABPNavigationProvider>();

            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
