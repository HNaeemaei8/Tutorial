using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.Models;
using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;

            var fileTypes = applicationDbContext.FileTypes.ToList();
            foreach (var item in fileTypes)
            {
                FileTypeViewModel newModel = new FileTypeViewModel()
                {
                    ID = item.ID,
                    Title = item.Title,
                    ActionName = ""
                };

                FileTypes.Add(newModel);
            }

            Categories = applicationDbContext.Categories.ToList(); 
        }
        public List<FileTypeViewModel> FileTypes { get; set; } = new List<FileTypeViewModel>();
        public List<Category> Categories { get; set; } = new List<Category>();


        protected ApplicationDbContext ApplicationDbContext { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["FileTypes"] = FileTypes;
            ViewData["Categories"] = Categories;
            base.OnActionExecuting(context);
        }
    }
}
