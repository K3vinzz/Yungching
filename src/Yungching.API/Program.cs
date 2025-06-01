using Yungching.Application;
using Yungching.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddSingleton<IDbConnectionFactory>(sp =>
// {
//     var configuration = sp.GetRequiredService<IConfiguration>();
//     var connectionString = configuration.GetConnectionString("DefaultConnection");
//     return new SqlConnectionFactory(connectionString!);
// });
// builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
