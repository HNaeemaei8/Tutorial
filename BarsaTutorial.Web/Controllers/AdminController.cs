using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.Models;
using BarsaTutorial.Web.Models.ViewModels;
using BarsaTutorial.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly EducationService _educationService;
        private readonly CategoryService _categoryService;
        private readonly FileTypeService _fileTypeService;

        public AdminController(ILogger<AdminController> logger,
            ApplicationDbContext applicationDbContext,
            EducationService educationService,
            CategoryService categoryService,
            FileTypeService fileTypeService
            ) : base(applicationDbContext)
        {
            _educationService = educationService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
        }
         

        public IActionResult Index()
        {
            return View();
        }
       public async Task<IActionResult> Educations()
        {
            var request = new GetEducationsRequest();

            var educations = await _educationService.GetEducations(request);
            return View("EducationList", educations);
        }

        [HttpGet]
        public async Task<IActionResult> AddEducation()
        {
            var categories = await _categoryService.GetCategories();
            var fileTypes = await _fileTypeService.GetFileTypes();
            ViewBag.Categories1 = new SelectList(categories,nameof(Category.ID),nameof(Category.Title));
            ViewBag.FileTypes1 = new SelectList(fileTypes, nameof(FileType.ID), nameof(FileType.Title));
            return View();
        }

        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> AddEducation([FromForm] NewEducationRequest newEducation)
        {
            var education = await _educationService.AddEducation(newEducation);

            return RedirectToAction(nameof(Educations));
        }


        [HttpGet]
        public async Task<IActionResult> EditEducation(int id)
        {
            var categories = await _categoryService.GetCategories();
            var fileTypes = await _fileTypeService.GetFileTypes();
            var education = await _educationService.GetEducation(id);
            ViewBag.Categories1 = new SelectList(categories, nameof(Category.ID), nameof(Category.Title));
            ViewBag.FileTypes1 = new SelectList(fileTypes, nameof(FileType.ID), nameof(FileType.Title));
            return View(education);
        }

        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> EditEducation(int id, [FromForm] EditEducationRequest newEducation)
        {
            var education = await _educationService.EditEducation(id, newEducation);

            return RedirectToAction(nameof(Educations));
        }
    }
}
