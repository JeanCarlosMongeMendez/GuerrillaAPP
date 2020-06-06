using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GuerrillaAPP.Controllers
{
    public class GuerrillaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            //Aca retornar Guerrilla
            IEnumerable<Object> students = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44338/api/guerrilla");
                try
                {
                    var responseTask = client.GetAsync("Get");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Object>>();
                        readTask.Wait();
                        students = readTask.Result;
                    }
                    else
                    {
                        students = Enumerable.Empty<Object>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
                    }
                }
                catch
                {
                    throw;
                }
            }
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}
