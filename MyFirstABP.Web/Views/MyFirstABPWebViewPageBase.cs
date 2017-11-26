using Abp.Web.Mvc.Views;

namespace MyFirstABP.Web.Views
{
    public abstract class MyFirstABPWebViewPageBase : MyFirstABPWebViewPageBase<dynamic>
    {

    }

    public abstract class MyFirstABPWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MyFirstABPWebViewPageBase()
        {
            LocalizationSourceName = MyFirstABPConsts.LocalizationSourceName;
        }
    }
}