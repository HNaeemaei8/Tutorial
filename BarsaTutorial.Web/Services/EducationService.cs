using BarsaTutorial.Web.Data;
using BarsaTutorial.Web.Models.Models;
using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Services
{
    public class EducationService
    {
        public EducationService()
        {

        }

        public EducationService(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            ApplicationDbContext = applicationDbContext;
            WebHostEnvironment = webHostEnvironment;
        }

        public ApplicationDbContext ApplicationDbContext { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public async Task<IEnumerable<GetEducationsResponse>> GetEducations(GetEducationsRequest request)
        {
            var query = ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons)
                .AsNoTracking()
                .AsQueryable();

            if (request.CategoryID.HasValue)
                query = query.Where(e => e.CategoryID == request.CategoryID.Value);

            if (request.FileTypeID.HasValue)
                query = query.Where(e => e.FileTypeID == request.FileTypeID.Value);

            if (request.ID.HasValue)
                query = query.Where(e => e.ID == request.ID.Value);

            if (!string.IsNullOrEmpty(request.Title))
                query = query
                        .Where(p => EF.Functions.Like(p.Title, $"%{request.Title}%") ||
                        EF.Functions.Like(p.FileType.Title, $"%{request.Title}%") ||
                        EF.Functions.Like(p.Category.Title, $"%{request.Title}%"));
            //query = query.Where(e => e.Title.Contains(request.Title));

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

                responseList.Add(edu);
            }
            return responseList;
        }

        public async Task<EditEducationRequest> GetEducation(int id)
        {
            var education = await ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).AsNoTracking().SingleAsync(e => e.ID == id);

            var edu = new EditEducationRequest
            {
                CategoryID = education.CategoryID,
                FileTypeID = education.FileTypeID,
                ID = education.ID,
                Title = education.Title,
            };

            foreach (var item in education.Lessons)
            {
                var lsn = new EditLessonRequest
                {
                    ID = item.ID,
                    Title = item.Title,
                    OldFileAddress = item.FileAddress,
                    LessonCode = item.LessonCode
                };

                edu.Lessons.Add(lsn);
            }

            return edu;
        }

        public async Task<Education> AddEducation(NewEducationRequest request)
        {
            var education = new Education
            {
                Title = request.Title,
                CategoryID = request.CategoryID,
                FileTypeID = request.FileTypeID
            };

            foreach (var item in request.Lessons)
            {
                var lesson = new Lesson
                {
                    Title = item.Title,
                    FileAddress = await UploadFile(item.FileAddress),
                    LessonCode = item.LessonCode
                };
                education.Lessons.Add(lesson);
            }

            await ApplicationDbContext.Educations.AddAsync(education);
            await ApplicationDbContext.SaveChangesAsync();
            return education;
        }

        public async Task<Education> EditEducation(int id, EditEducationRequest request)
        {
            var education = await ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).SingleAsync(e => e.ID == id);

            education.Title = request.Title;
            education.CategoryID = request.CategoryID;
            education.FileTypeID = request.FileTypeID;

            foreach (var item in request.Lessons)
            {
                var lesson = education.Lessons.SingleOrDefault(e => e.ID == item.ID);

                if (lesson is null)
                {
                    var newLesson = new Lesson
                    {
                        Title = item.Title,
                        FileAddress = await UploadFile(item.FileAddress),
                        LessonCode = item.LessonCode
                    };
                    education.Lessons.Add(newLesson);
                }
                else
                {
                    lesson.LessonCode = item.LessonCode;
                    var file =  await UploadFile(item.FileAddress);
                    if (!string.IsNullOrEmpty(file))
                        lesson.FileAddress = file;
                    lesson.Title = item.Title;

                }
            }

            ApplicationDbContext.Educations.Update(education);
            await ApplicationDbContext.SaveChangesAsync();

            return education;
        }

        public async Task<int> RemoveEducation(int id)
        {
            var education = new Education
            {
                ID = id
            };

            ApplicationDbContext.Educations.Remove(education);

            return await ApplicationDbContext.SaveChangesAsync();

        }

        public async Task<Education> AddLessonToEducation(int id, NewLessonRequest request)
        {

            var education = await ApplicationDbContext.Educations
                .Include(e => e.Lessons)
                .SingleAsync(e => e.ID == id);

            var lesson = new Lesson
            {
                Title = request.Title,
                FileAddress = await UploadFile(request.FileAddress),
                LessonCode = request.LessonCode
            };
            education.Lessons.Add(lesson);

            ApplicationDbContext.Educations.Update(education);
            await ApplicationDbContext.SaveChangesAsync();

            return education;

        }

        public async Task<int> RemoveLessonFromEducation(int id, int lessonID)
        {

            var lesson = new Lesson
            {
                ID = lessonID
            };

            ApplicationDbContext.Lessons.Remove(lesson);

            return await ApplicationDbContext.SaveChangesAsync();

        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                //upload files to wwwroot
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(WebHostEnvironment.WebRootPath, "files", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }
                //your logic to save filePath to database, for example

                return Path.Combine("files", fileName);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}