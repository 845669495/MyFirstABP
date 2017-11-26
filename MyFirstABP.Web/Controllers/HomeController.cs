using Abp.Dependency;
using Abp.Domain.Repositories;
using MyFirstABP.EntityFramework.Repositories;
using System.Web.Mvc;

namespace MyFirstABP.Web.Controllers
{
    public class HomeController : MyFirstABPControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}