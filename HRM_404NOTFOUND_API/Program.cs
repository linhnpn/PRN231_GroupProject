using GroupProject_HRM_Api.Middlewares;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Profiles;
using GroupProject_HRM_Library.Repository.Implement;
using GroupProject_HRM_Library.Repository.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opts
                    => opts.SuppressModelStateInvalidFilter = true)
                .AddJsonOptions(options
                    => options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter()))
                ;

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();

builder.Services.AddAutoMapper(typeof(EmployeeProfile), typeof(TaxProfile));

//builder.Services.AddTransient<ExceptionMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
