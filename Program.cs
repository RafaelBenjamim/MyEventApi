using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MyEventApi.Application.Services;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;
using MyEventApi.Infrastructure.Repository;
using MyEventApi.Infrastructure.Services;
using Resend;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://fiorella-club.vercel.app").AllowAnyHeader().AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

// HttpContext
builder.Services.AddHttpContextAccessor();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Infrastructure Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IEmailService, ResendEmailService>();

// Payment Gateways
builder.Services.AddHttpClient<InfinitePayPaymentGateway>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["InfinitePay:BaseUrl"]
        ?? throw new InvalidOperationException("InfinitePay:BaseUrl não configurado."));
});

builder.Services.AddResend(options =>
{
    options.ApiToken = builder.Configuration["Resend:ApiKey"];
});

builder.Services.AddScoped<InfinitePayPaymentGateway>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient(nameof(InfinitePayPaymentGateway));
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new InfinitePayPaymentGateway(httpClient, configuration);
});
builder.Services.AddScoped<PagBankGateway>();
builder.Services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();

// Application Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IEmailLogRepository, EmailLogRepository>();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();