using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dachii.Models;

namespace dachii.Controllers
{
    public class DachiiController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<Dachii>("dachii") == null)
            {
                HttpContext.Session.SetObjectAsJson("dachii", new Dachii());
            }
            ViewBag.dachii = HttpContext.Session.GetObjectFromJson<Dachii>("dachii");
            return View();
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string Activity)
        {
            Dachii curDachi = HttpContext.Session.GetObjectFromJson<Dachii>("dachii");
            if (Activity == "Feed")
            {
                curDachi.feed();
            }
            else if (Activity == "Work")
            {
                curDachi.work();
            }
            else if (Activity == "Play")
            {
                curDachi.play();
            }
            else if (Activity == "Sleep")
            {
                curDachi.sleep();
            }
            HttpContext.Session.SetObjectAsJson("dachii", curDachi);
            // ViewBag.dachii = curDachi;
            return RedirectToAction("Index");
        }
    }
}