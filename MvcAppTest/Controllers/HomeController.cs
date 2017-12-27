using Microsoft.AspNet.SignalR;
using MvcAppTest.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult InvokeHub()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvokeHub(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SystemHub>();
            context.Clients.All.hello(message);
            ViewBag.Result = "消息：“" + message + "”已推送";
            return View();
        }
    }
}
