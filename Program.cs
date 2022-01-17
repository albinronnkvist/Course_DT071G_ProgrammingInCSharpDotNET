using ForumAPI.Data;
using ForumAPI.Repositories.PostRepository;
using ForumAPI.Repositories.UserRepository;
using ForumAPI.UserSecurity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext.
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// Add services for controllers.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure swagger to work with JWT Bearer.
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Register AutoMapper.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories.
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register JWT Bearer.
var signingKey = System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings:SigningKey").Value);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Register token handler.
builder.Services.AddSingleton<SecureToken>();

// Register HttpContextAccessor.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.

// Use Swagger during development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable authentication and authorization.
app.UseAuthentication();
app.UseAuthorization();

// Add endpoints for controller actions.
app.MapControllers();

// Run the application.
app.Run();