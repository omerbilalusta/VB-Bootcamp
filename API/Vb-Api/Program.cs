using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Vb_Base.Token;
using Vb_Bootcamp.Middlewares;
using Vb_Bootcamp.Services;
using Vb_Data.Context;
using Vb_Data.UnitOfWork;
using Vb_Operation.Cqrs;
using Vb_Operation.Mapping;
using Vb_Operation.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://localhost:4200")  // Allow requests from this origin
            .AllowAnyMethod()                       // Allow all HTTP methods
            .AllowAnyHeader()                       // Allow all HTTP headers
            .AllowCredentials()                     // Allow credentials (cookies, etc.)
        );
});

string connection = builder.Configuration.GetConnectionString("MsSqlConnection");
builder.Services.AddDbContext<VbDbContext>(opts => opts.UseSqlServer(connection));

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(typeof(CreateTokenCommandDealer).GetTypeInfo().Assembly);

var JwtConfig = builder.Configuration.GetSection("JwtConfig").Get<Vb_Base.Token.JwtConfig>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
builder.Services.AddSingleton(config.CreateMapper());

builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Supplier API Management", Version = "v1.0" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Supplier API Management for Cinema Company",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = JwtConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
        ValidAudience = JwtConfig.Audience,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
