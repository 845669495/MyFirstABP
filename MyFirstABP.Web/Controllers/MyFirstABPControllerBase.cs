using Abp.Web.Mvc.Controllers;

namespace MyFirstABP.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class MyFirstABPControllerBase : AbpController
    {
        protected MyFirstABPControllerBase()
        {
            LocalizationSourceName = MyFirstABPConsts.LocalizationSourceName;
        }
    }
}