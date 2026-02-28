using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WarehouseManagement.Api;
using WarehouseManagement.Api.Middlewares;
using WarehouseManagement.Application;
using WarehouseManagement.Infrastructure;
using WarehouseManagement.Infrastructure.Persistence;
using WarehouseManagement.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký các dịch vụ từ các tầng (Layer)
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Cấu hình xác thực JWT (Bắt buộc phải có để API hiểu được Token)
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    // 1. Định nghĩa kiểu bảo mật là Bearer Token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Vui lòng nhập Token theo định dạng: Bearer {your_token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // 2. Áp dụng bảo mật này cho tất cả các API
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// 3. Tự động Migrate và Seed Data khi khởi chạy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        await SeedData.SeedAsync(db);
    }
    catch (Exception ex)
    {
        // Log lỗi nếu migrate thất bại (ví dụ: sai chuỗi kết nối DB)
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Đã xảy ra lỗi trong quá trình Migration hoặc Seed dữ liệu.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. THỨ TỰ QUAN TRỌNG: Authentication phải đứng TRƯỚC Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<StoreContextMiddleware>();

app.MapControllers();

app.Run();