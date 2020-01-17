using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_LMS.Controllers
{
    public class CopiesController : Controller
    {
        // GET: Copies
        public ActionResult Index()
        {
            return View();
        }

        // GET: Copies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Copies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Copies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Copies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Copies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Copies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Copies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}