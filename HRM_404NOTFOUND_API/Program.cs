using GroupProject_HRM_Api.Middlewares;
using GroupProject_HRM_Library.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opts
                    => opts.SuppressModelStateInvalidFilter = true);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();

//builder.Services.AddAutoMapper(typeof(ProductProfile), typeof(CategoryProfile));

builder.Services.AddTransient<ExceptionMiddleware>();

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

app.ConfigureExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
