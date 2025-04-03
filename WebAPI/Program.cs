




using BLL.Services;
using DAL.Api;
using DAL.Service;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<ISuppliersService, SuppliersService>();
builder.Services.AddScoped<IGoodsToSuppliersService, GoodsToSuppliersService>();
builder.Services.AddScoped<IGoodsToOrderService, GoodsToOrderService>();

builder.Services.AddScoped<IOrderManager, OrderManager>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
