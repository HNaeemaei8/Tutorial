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
    public class EducationService
    {
        public EducationService()
        {

        }

        public EducationService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<IEnumerable<GetEducationsResponse>> GetEducations(GetEducationsRequest request)
        {
            var query = ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).AsNoTracking().AsQueryable();

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
            return responseList;
        }

        public async Task<Education> GetEducation(int id)
        {
            var education = await ApplicationDbContext.Educations
                .Include(e => e.Category)
                .Include(e => e.FileType)
                .Include(e => e.Lessons).AsNoTracking().SingleAsync(e => e.ID == id);

            return education;
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
                    FileAddress = item.FileAddress,
                    LessonCode = item.LessonCode
                };
                education.Lessons.Append(lesson);
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
                        FileAddress = item.FileAddress,
                        LessonCode = item.LessonCode
                    };
                    education.Lessons.Append(newLesson);
                }
                else
                {
                    lesson.LessonCode = item.LessonCode;
                    lesson.FileAddress = item.FileAddress;
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
                FileAddress = request.FileAddress,
                LessonCode = request.LessonCode
            };
            education.Lessons.Append(lesson);

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
    }
}