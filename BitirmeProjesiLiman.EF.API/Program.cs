using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BitirmeProjesiLiman.Data.EF.Context;
using BitirmeProjesiLiman.Core.Repositories;
using BitirmeProjesiLiman.Data.EF.Repositories;
using BitirmeProjesiLiman.Data.EF.UnitOfWork;
using BitirmeProjesiLiman.Service.Services;
using BitirmeProjesiLiman.Service.Caching;
using Serilog;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Serilog Yapılandırması
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/portmaster_ef_log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

// 2. Swagger Yapılandırması (JWT Desteği ile)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PortMaster EF Write API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Lütfen JWT Token'ınızı buraya girin. Örn: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// 3. EF Core Bağlantı Tanımı
builder.Services.AddDbContext<BitirmeProjesiLimanDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=portmaster.db"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// 4. İş Mantığı (Service) ve Önbellek Kayıtları
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient<IAIService, AIService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();

// 5. JWT Bearer Kimlik Doğrulama Yapılandırması
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "PortMasterSuperSecretKey1234567890!!!";
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Otomatik Veritabanı Oluşturma ve Seed Data Tetikleme
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BitirmeProjesiLimanDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortMaster EF API v1"));
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Static Files serving (React arayüzü için)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
