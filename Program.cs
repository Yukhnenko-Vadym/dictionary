using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using DictionaryApp.Features.UsersAuth.Models.Domain;
using DictionaryApp.Features.UsersAuth.Service.Implementations;
using DictionaryApp.Features.UsersAuth.Service.Interfaces;
using DictionaryApp.Features.UsersAuth.Repository.Interface;
using DictionaryApp.Features.UsersAuth.Repository.Implementations;
using DictionaryApp.Data;


using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var config = builder.Configuration;

builder.Services.AddTransient<IElasticsearchService<WordElastic>, ElasticsearchService>();

// Register WordService and WordRepository (which depend on ElasticsearchService)
builder.Services.AddTransient<IWordsRepository<Word>, WordsRepository>();
builder.Services.AddTransient<IWordService<Word>, WordService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUserRepository, UsersRepository>();
//builder.Services.AddTransient<IHeadersService, HeadersService>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();



builder.Services.AddScoped<ElasticsearchClient>(provider =>
{
   var url = builder.Configuration["Elasticsearch:Url"];
   return new ElasticsearchClient(new Uri(url));
});

// Register DbContext
builder.Services.AddScoped<DbContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    var databaseName = builder.Configuration["DatabaseName"];
    return new DbContext(connectionString, databaseName);
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.RequireHttpsMetadata = false;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]!)),
        ValidateIssuer = true,
        ValidIssuer = config["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = config["JWT:ValidAudience"],
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true //added
    };
});

builder.Services.AddAuthorization();//added

// Register Controllers
builder.Services.AddControllers(opts => opts.Filters.Add<ApiExceptionFilter>());  

// Build the application
var app = builder.Build();

// Configure middleware and endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
