using HenwoniDataModifierAPI.Data;
using HenwoniDataModifierAPI.Automatic;
using HenwoniDataModifierAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HenwoniDataModifierAPI;
using HenwoniDataModifierAPI.Areas.User.SystemServices;
using HenwoniDataModifierAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => {
        // options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    }).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();
builder.Services.AddHostedService<AutomaticSetup>();
// IJWTTokenService
builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
        .AddJwtBearer(p =>
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:Key"]);
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWTToken:Issuer"],
                ValidAudience = builder.Configuration["JWTToken:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); ///ADDED
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
