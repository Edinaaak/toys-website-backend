using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NBP_projekat.Mapping;
using System.Text;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;
using UmetnickaDela.Infrastructure.Repositories;
using UmetnickaDela.Infrastructure;
using NBP_projekat.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UmetnickaDela.Data.DataContext>(options => options.UseSqlServer(@"Data Source=SQL8003.site4now.net;Initial Catalog=db_a9a911_naprednebaze;User Id=db_a9a911_naprednebaze_admin;Password=Edinaa1!;"));
builder.Services.AddIdentity<User, AppRole>()
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(ApplicationMapping));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IMestoRepository, MestoRepository>();
builder.Services.AddScoped<IUmetnickoDelo, UmetnickoDeloRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(Program)));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
    )
    .AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = "http://localhost:5001",
            ValidIssuer = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTK"))
        };
    }
    );
var app = builder.Build();

// Configure the HTTP request pi
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

using (var serviceScope = app.Services.CreateScope())
{
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var adminRole = new AppRole("Admin");
        await roleManager.CreateAsync(adminRole);
    }

    if (!await roleManager.RoleExistsAsync("Slikar"))
    {
        var slikarRole = new AppRole("Slikar");
        await roleManager.CreateAsync(slikarRole);
    }

    if (!await roleManager.RoleExistsAsync("Ziri"))
    {
        var ziriRole = new AppRole("Ziri");
        await roleManager.CreateAsync(ziriRole);
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
