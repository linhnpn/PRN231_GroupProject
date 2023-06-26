using Google.Cloud.Storage.V1;
using GroupProject_HRM_Api.Middlewares;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Profiles;
using GroupProject_HRM_Library.Repository.Implement;
using GroupProject_HRM_Library.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opts
                    => opts.SuppressModelStateInvalidFilter = true);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILeaveLogRepository, LeaveLogRepository>();

builder.Services.AddAutoMapper(typeof(EmployeeProfile), typeof(LeaveLogProfile));

builder.Services.AddTransient<ExceptionMiddleware>();
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "serviceFirebase.json");
builder.Services.AddSingleton<IFirebaseStorageService>(s => new FirebaseStorageService(StorageClient.Create()));

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
