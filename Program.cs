using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Repositories; // Needed for the repositories

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register Repositories (THIS WAS MISSING)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// 3. Add Controller Services
builder.Services.AddControllers();

// 4. Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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