using BarsaTutorial.Web.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BarsaTutorial.Web.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any board games.
                if (context.FileTypes.Any())
                {
                    return;   // Data was already seeded
                }

                context.FileTypes.AddRange(
                    new FileType
                    {
                        Title = "PDF"
                    },
                    new FileType
                    {
                        Title = "Video"
                    }
                    //new FileType
                    //{
                    //    Title = "MP3"
                    //}
                    );

                context.Categories.AddRange(
                    new Category
                    {
                        Title = "برسا"
                    },
                    new Category
                    {
                        Title = "دیتابیس"
                    }
                    //new Category
                    //{
                    //    Title = "بیزنس بانکی"
                    //}
                    );

                context.SaveChanges();
            }
        }
    }
}
