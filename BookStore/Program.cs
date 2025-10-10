using BookStore.Data; // Đây là namespace của ApplicationDbContext
// Dòng 'using' này dùng để lấy class GetAllAuthorsQuery làm mốc cho MediatR
//using BookStore.Application.Feature.Authors.Queries.GetAllAuthorsQuery;
using BookStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Features.Authors.Queries.GetAllAuthors;

var builder = WebApplication.CreateBuilder(args);

// === ĐĂNG KÝ SERVICES VÀO CONTAINER ===

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DbContext và Interface của nó
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

// === ĐÂY LÀ DÒNG SỬA LỖI QUAN TRỌNG NHẤT ===
// Đăng ký MediatR với cú pháp của phiên bản 12+
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

app.UseAuthorization();

app.MapControllers();

app.Run();