using BookStore.Data; // <<< SỬA LẠI NAMESPACE CHO ĐÚNG
using BookStore.Application.Features.Authors.Queries.GetAllAuthors;
using BookStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === ĐĂNG KÝ SERVICES VÀO CONTAINER ===

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Cấu hình để xử lý các vòng lặp tham chiếu
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DbContext và Interface của nó
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// Đảm bảo ApplicationDbContext đã implement IApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

// Đăng ký CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Đăng ký MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllAuthorsQuery).Assembly));


var app = builder.Build();

// === CẤU HÌNH HTTP REQUEST PIPELINE ===

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// === DI CHUYỂN UseCors LÊN TRÊN ĐỂ ĐẢM BẢO ƯU TIÊN ===
// Thứ tự đúng: Routing -> CORS -> Auth
app.UseRouting(); // Thêm dòng này để định nghĩa rõ ràng pipeline

app.UseCors("ReactAppPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();