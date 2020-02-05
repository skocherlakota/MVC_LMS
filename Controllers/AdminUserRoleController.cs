using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVC_LMS.Controllers
{
    public class AdminUserRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}