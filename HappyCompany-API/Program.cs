using CoreRepository;
using DataRepository;
using DataRepository.Context;
using HappyCompany_API.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Allow Specific Origins (url)
string AllowSpecificOrigins = "_Origin";



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//sqlite
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    , b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
    ));

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = false,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
             });


//Origins
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      builder =>
                      {
                          //antoher probelm with cross Origins
                          //builder.WithOrigins("https://localhost:7098/")
                          builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

//loggrer
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IJWT, JWT>();
builder.Services.AddTransient<DataSeeder>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedDatabase(app);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(AllowSpecificOrigins);
app.MapControllers();

app.Run();


void SeedDatabase(IHost app)
{
    using (var Scope = app.Services.CreateScope())
    {
        var DBSeeder = Scope.ServiceProvider.GetRequiredService<DataSeeder>();
        DBSeeder.Seed();
    }
}
