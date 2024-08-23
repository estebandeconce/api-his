using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhostDevelopment",
      policy =>
      {
        policy.WithOrigins("http://localhost:8080", "http://localhost:8040")
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowLocalhostDevelopment");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//controller va sin home en el ejemplo de internet
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action}"
  );

app.Run();
