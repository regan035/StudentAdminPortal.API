using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Cors policy to frontend
builder.Services.AddCors((options) =>
{
    options.AddPolicy("ngUI", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET","POST","PUT","DELETE")
        .WithExposedHeaders("*");
});
});

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject EntityFramwork DBContex
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//Inject Repo Pattern
builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();

//Inject AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Set local storage resource folder to static -->render stored profile image from resource to UI
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"Resources")),
    RequestPath = "/Resources"
});

app.UseRouting();

app.UseCors("ngUI");

app.UseAuthorization();

app.MapControllers();

app.Run();
