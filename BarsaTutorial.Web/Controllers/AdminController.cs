using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public IActionResult Index()
        {
            return View();
        }



    }
}
