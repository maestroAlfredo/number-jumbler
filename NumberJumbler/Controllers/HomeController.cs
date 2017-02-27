using NumberJumbler.Models;
using NumberJumbler.Services;
using NumberJumbler.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NumberJumbler.Controllers
{
    public class HomeController : Controller
    {
        INumberJumblerWebService service;

        public HomeController(INumberJumblerWebService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View(new NumberJumblerHomeViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Upload(bool validate=false)
        {
            var result = await service.FindLeader(Request, validate);
            using (var input = (Request.InputStream))
            {
                using(var reader = new StreamReader(input))
                {
                    var pos = input.Position;
                    string resultstring = reader.ReadToEnd();
                }
            }

                return View("Index", new NumberJumblerHomeViewModel() {  Result = result });
        }
    }
}
