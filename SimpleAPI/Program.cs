using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data.Context;
using SimpleAPI.Data.Entities;
using SimpleAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserRepository, UserRepository>();
string connString = builder.Configuration.GetConnectionString("APIConnection");
builder.Services.AddDbContext<SimpleAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIConnection"));

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
