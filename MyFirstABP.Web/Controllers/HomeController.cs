using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using MyFirstABP.EntityFramework.Repositories;
using System.Web.Mvc;

namespace MyFirstABP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MyFirstABPControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}