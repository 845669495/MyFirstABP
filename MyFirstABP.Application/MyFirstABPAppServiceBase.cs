using Abp.Application.Services;

namespace MyFirstABP
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MyFirstABPAppServiceBase : ApplicationService
    {
        protected MyFirstABPAppServiceBase()
        {
            LocalizationSourceName = MyFirstABPConsts.LocalizationSourceName;
        }
    }
}