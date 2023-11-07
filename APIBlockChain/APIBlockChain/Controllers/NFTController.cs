using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBlockChain.Controllers
{
    public class NFTController : Controller
    {
        // GET: NFTController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NFTController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NFTController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NFTController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NFTController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NFTController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NFTController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NFTController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
