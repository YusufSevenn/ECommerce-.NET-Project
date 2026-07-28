using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Contexts;
using ECommerce.Core.Entities;
using FluentValidation.AspNetCore;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Application.Services;
using ECommerce.Application.Mapping;
using ECommerce.Interfaces;
using FluentValidation;
using ECommerce.Applicaiton.Validations;
using ECommerce.API.Filters;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MapProfile>();
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Yazdığımız ValidationFilter'ı tüm controller'lar için global olarak ekliyoruz
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidationFilter());
});

//.NET'in kendi varsayılan doğrulama (validation) mekanizmasını devre dışı bırakıyoruz ki
//yazdığımız ValidationFilter devreye girebilsin.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();