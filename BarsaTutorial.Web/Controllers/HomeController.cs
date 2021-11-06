using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models;
using BarsaTutorial.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BarsaTutorial.Web.Services;
using BarsaTutorial.Web.Models.ViewModels;

namespace BarsaTutorial.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EducationService _educationService;
        public FileTypeService Service { get; }

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, FileTypeService service, EducationService education) : base(dbContext)
        {
            _logger = logger;
            Service = service;
            _educationService = education;
            ViewData["FileTypes"] = FileTypes;
        }

        public async Task<IActionResult> Index()
        {
            var request = new GetEducationsRequest();

            var educations = await _educationService.GetEducations(request);
            return View("Education", educations);
        }

        public async Task<IActionResult> GetFileType(int id)
        {
            var request = new GetEducationsRequest
            {
                FileTypeID = id
            };

            var educations = await _educationService.GetEducations(request);

            return View("Education", educations);
        }

        public async Task<IActionResult> GetCategory(int id)
        {
            var request = new GetEducationsRequest
            {
                CategoryID = id
            };

            var educations = await _educationService.GetEducations(request);

            return View("Education", educations);
        }

        public async Task<IActionResult> Search(string query)
        {
            var request = new GetEducationsRequest
            {
                Title = query
            };
            return View("Education", await _educationService.GetEducations(request));
        }

    }
}
