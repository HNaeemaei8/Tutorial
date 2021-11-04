using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> GetEducations(GetEducationsRequest request)
        {
            var query = ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).AsQueryable();

            if (request.CategoryID.HasValue)
                query = query.Where(e => e.CategoryID == request.CategoryID.Value);

            if (request.FileTypeID.HasValue)
                query = query.Where(e => e.FileTypeID == request.FileTypeID.Value);

            if (request.ID.HasValue)
                query = query.Where(e => e.ID == request.ID.Value);

            if (string.IsNullOrEmpty(request.Title))
                query = query.Where(e => e.Title.Contains(request.Title));

            var educations = await query.ToListAsync();

            var responseList = new List<GetEducationsResponse>();

            foreach (var item in educations)
            {
                var edu = new GetEducationsResponse
                {
                    ID = item.ID,
                    CategoryID = item.CategoryID,
                    CategoryTitle = item.Category.Title,
                    Title = item.Title,
                    FileTypeID = item.FileTypeID,
                    FileTypeTitle = item.FileType.Title,
                    LessonCount = item.Lessons.Count()
                };

                responseList.Add(edu);
            }
            return View(responseList);
        }

    }
}
