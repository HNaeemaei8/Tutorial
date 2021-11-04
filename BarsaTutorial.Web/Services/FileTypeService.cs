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

    public class FileTypeService
    {
        public FileTypeService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<IEnumerable<FileType>> GetFileTypes()
        {
            var fileTypes = await ApplicationDbContext.FileTypes.AsNoTracking().ToListAsync();

            return fileTypes;
        }

        public async Task<GetFileTypeEdicationResponse> GetFileTypeEducations(int typeId)
        {
            var fileType = await ApplicationDbContext.FileTypes.SingleOrDefaultAsync(e => e.ID == typeId);

            var educations = await ApplicationDbContext.Educations
                .Include(e => e.FileType)
                .Include(e => e.Category)
                .Include(e => e.Lessons).Where(e => e.FileTypeID == typeId).ToListAsync();

            var cat = new GetFileTypeEdicationResponse
            {
                ID = fileType.ID,
                Title = fileType.Title
            };

            foreach (var item in educations)
            {
                var edu = new GetEducationsResponse
                {
                    ID = item.ID,
                    FileTypeID = item.FileTypeID,
                    FileTypeTitle = item.FileType.Title,
                    Title = item.Title,
                    CategoryID = item.CategoryID,
                    CategoryTitle = item.Category.Title,
                    LessonCount = item.Lessons.Count()
                };

                cat.Educations.Append(edu);
            }

            return cat;
        }

        public async Task<FileType> AddFileType(NewFileTypeRequest request)
        {
            var fileType = new FileType
            {
                Title = request.Title
            };

            await ApplicationDbContext.FileTypes.AddAsync(fileType);
            await ApplicationDbContext.SaveChangesAsync();
            return fileType;
        }

        public async Task<FileType> EditFileType(int id, EditFileTypeRequest request)
        {
            var fileType = await ApplicationDbContext.FileTypes.SingleAsync(e => e.ID == id);

            fileType.Title = request.Title;

            ApplicationDbContext.FileTypes.Update(fileType);
            await ApplicationDbContext.SaveChangesAsync();

            return fileType;
        }

        public async Task<int> RemoveFileType(int id)
        {
            var fileType = new FileType
            {
                ID = id
            };

            ApplicationDbContext.FileTypes.Remove(fileType);

            return await ApplicationDbContext.SaveChangesAsync();

        }

    }
}
