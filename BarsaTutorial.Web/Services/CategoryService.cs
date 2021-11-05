using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.Models;
using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Services
{
    public class CategoryService
    {
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await ApplicationDbContext.Categories.AsNoTracking().ToListAsync();

            return categories;
        }

        public async Task<GetCategoryEdicationResponse> GetCategoryEducations(int catId)
        {
            var category = await ApplicationDbContext.Categories.SingleOrDefaultAsync(e => e.ID == catId);

            var educations = await ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).Where(e => e.CategoryID == catId).ToListAsync();

            var cat = new GetCategoryEdicationResponse
            {
                ID = category.ID,
                Title = category.Title
            };

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

                foreach (var les in item.Lessons)
                {
                    var lsn = new LessonViewModel()
                    {
                        ID = les.ID,
                        FileAddress = les.FileAddress,
                        LessonCode = les.LessonCode,
                        Title = les.Title
                    };
                    edu.Lessons.Add(lsn);
                }

                cat.Educations.Add(edu);
            }

            return cat;
        }

        public async Task<Category> AddCategory(NewCategoryRequest request)
        {
            var category = new Category
            {
                Title = request.Title
            };

            await ApplicationDbContext.Categories.AddAsync(category);
            await ApplicationDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> EditCategory(int id, EditCategoryRequest request)
        {
            var category = await ApplicationDbContext.Categories.SingleAsync(e => e.ID == id);

            category.Title = request.Title;

            ApplicationDbContext.Categories.Update(category);
            await ApplicationDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<int> RemoveCategory(int id)
        {
            var category = new Category
            {
                ID = id
            };

            ApplicationDbContext.Categories.Remove(category);

            return await ApplicationDbContext.SaveChangesAsync();

        }

    }
}