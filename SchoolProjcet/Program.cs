
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.Reposatories.Implmentation;
using SchoolProjcet.Reposatories.Interfaces;
namespace SchoolProjcet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<StudentRepo>();
            builder.Services.AddScoped<IGenaricRepo<Student>, GanaricRepo<Student>>();
            builder.Services.AddScoped(typeof(IGenaricRepo<Department>), typeof(GanaricRepo<Department>));
            builder.Services.AddScoped(typeof(IGenaricRepo<Subject>), typeof(GanaricRepo<Subject>));
            builder.Services.AddScoped(typeof(IGenaricRepo<Teacher>), typeof(GanaricRepo<Teacher>));
            builder.Services.AddScoped(typeof(IGenaricRepo<Enrollment>), typeof(GanaricRepo<Enrollment>));
            builder.Services.AddScoped(typeof(IGenaricRepo<ClassRoom>), typeof(GanaricRepo<ClassRoom>));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
