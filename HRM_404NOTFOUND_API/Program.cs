using Google.Cloud.Storage.V1;
using GroupProject_HRM_Api.Middlewares;
using GroupProject_HRM_Library.Constaints;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Profiles;
using GroupProject_HRM_Library.Repository.Implement;
using GroupProject_HRM_Library.Repository.Interface;
using System.Text.Json.Serialization;
using GroupProject_HRM_Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                /*.ConfigureApiBehaviorOptions(opts
                    => opts.SuppressModelStateInvalidFilter = true)*/
                .AddJsonOptions(options
                    => options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter()))
                ;

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILeaveLogRepository, LeaveLogRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOvertimeLogRepository, OvertimeLogRepository>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
builder.Services.AddScoped<IJWTServices, JWTServices>();

builder.Services.AddAutoMapper(typeof(EmployeeProfile), typeof(TaxProfile), typeof(LeaveLogProfile), typeof(EmployeeProjectProfile), typeof(ProjectProfile), typeof(IncomeProfile));

builder.Services.AddTransient<ExceptionMiddleware>();
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "serviceFirebase.json");
builder.Services.AddSingleton<IFirebaseStorageService>(s => new FirebaseStorageService(StorageClient.Create()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//config swagger 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation", Version = "v1" });

    // Cấu hình xác thực JWT cho Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };
    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constains.JWT_KEY)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectAPI v1"));
}


app.ConfigureExceptionMiddleware();
app.UseMiddleware<JWTMiddleWare>();

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
