using api.Data;
using api.Data.Models;
using api.Interfaces;
using api.Repository;
using api.Repository.Interfaces;
using api.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


Env.Load(".env");  // Si el archivo está dentro de la carpeta 'api'
// Verificar que la clave de firma se cargue correctamente
var signingKey = Environment.GetEnvironmentVariable("Signing_Key");
if (string.IsNullOrEmpty(signingKey))
{
    throw new InvalidOperationException("Signing key is not set in the environment variables.");
}

// Verificar también la conexión por si la variable está vacía
var defaultConnection = Environment.GetEnvironmentVariable("Default_Conection");
if (string.IsNullOrEmpty(defaultConnection))
{
    throw new InvalidOperationException("Default connection string is not set in the environment variables.");
}
var Issuer = Environment.GetEnvironmentVariable("Issuer");
if (string.IsNullOrEmpty(signingKey))
{
    throw new InvalidOperationException("Issuer not set in the environment variables.");
}

// Verificar también la conexión por si la variable está vacía
var Audience = Environment.GetEnvironmentVariable("Audience");
if (string.IsNullOrEmpty(defaultConnection))
{
    throw new InvalidOperationException("Audience is not set in the environment variables.");
}





var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseNpgsql(defaultConnection);  
});


///users

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 10;
})
.AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(Options =>{
    Options.DefaultAuthenticateScheme =
    Options.DefaultChallengeScheme =
    Options.DefaultForbidScheme =
    Options.DefaultScheme =
    Options.DefaultSignInScheme =
    Options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(signingKey)
            )
        };
});
///users


builder.Services.AddScoped<ItokenService,  TokenService>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IImplementosRepository, ImplementoRepository>();
builder.Services.AddScoped<IReservaAreaRepository, ReservaAreaRepository>();
builder.Services.AddScoped<IReservasImplementosRepository, ReservasImplementosRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Aplicar la política de CORS
app.UseCors("AllowAll");



app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyOrigin()  // Permite cualquier origen
    .AllowAnyMethod()  // Permite cualquier método (GET, POST, etc.)
    .AllowAnyHeader()  // Permite cualquier encabezado
    .AllowCredentials());  // Permite el envío de cookies y cabeceras de autenticación

///Useers
app.UseAuthentication();
app.UseAuthorization();
///Useers 
///
app.MapControllers();

app.Run();

