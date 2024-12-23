using api.Data;
using api.Data.Models;
using api.Interfaces;
using api.Repository;
using api.Repository.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS para cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Permite cualquier origen
              .AllowAnyHeader() // Permite cualquier encabezado
              .AllowAnyMethod(); // Permite cualquier método HTTP (GET, POST, etc.)
    });
});




builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
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
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));  
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

}).AddJwtBearer(Options => {

      var signingKey = builder.Configuration["JWT:SigningKey"];
    if (string.IsNullOrWhiteSpace(signingKey))
    {
        throw new ArgumentNullException("JWT:SigningKey", "La clave JWT no puede ser nula o vacía.");
    }

    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration ["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
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

///Useers
app.UseAuthentication();
app.UseAuthorization();
///Useers 
///
app.MapControllers();

app.Run();

