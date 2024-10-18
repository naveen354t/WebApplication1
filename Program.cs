using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
//using Microsoft.EntityFrameworkCore.SqlServerKeyExtensions

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();
//builder.Logging.AddLog4Net();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//implementation of Automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddTransient<IStudentReopository, StudentRepositiory>();
builder.Services.AddScoped(typeof(ICollegeRepository<>),typeof(CollegeRepository<>));
builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});
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
