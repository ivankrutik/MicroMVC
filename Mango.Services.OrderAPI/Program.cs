using AutoMapper;
using Mango.Services.OrderAPI.DbContexts;
using Mango.Services.OrderAPI.Messaging;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Repository;
using Mango.Servises.OrderAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBuilder.UseSqlServer(connectionString);
builder.Services.AddSingleton(new OrderRepository(optionBuilder.Options));

builder.Services.AddScoped<IServiseBusConsumer, ServiseBusConsumer>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = @"https://localhost:7150/";
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "mango");
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Service.OrderAPI", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference= new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var myDependency = services.GetRequiredService<IServiseBusConsumer>();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
