using Case.API.Exeception;
using Case.API.Mapper;
using Case.Infrastructure.Persistance;
using Case.Shared.Extensions;
using Case.Shared.Options;
using Case.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<CaseAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CaseAPIDb"));
});

builder.Services.ConfigureUnitOfWork();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();
app.UseCustomExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
