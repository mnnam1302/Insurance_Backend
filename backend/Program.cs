using AspNetCore.Email;
using AutoMapper;
using backend.Controllers;
using backend.Filters;
using backend.Helper;
using backend.IRepositories;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<InsuranceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsuranceConnectionString")));


// Repositories && Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IInsuranceTypeRepository, InsuranceTypeRepository>();
builder.Services.AddScoped<IInsuranceTypeService, InsuranceTypeService>();

builder.Services.AddScoped<IInsuranceRepository, InsuranceRepository>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();

builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();

builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();


builder.Services.AddScoped<IVerificationPasswordRepository, VerificationPasswordRepository>();
builder.Services.AddScoped<IVerificationPasswordService, VerificationPasswordService>();

builder.Services.AddScoped<IPaymentRequestRepository, PaymentRequestReponsitory>();
builder.Services.AddScoped<IPaymentRequestService, PaymentRequestService>();

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractService, ContractService>();

builder.Services.AddScoped<IBenefitDetailRepository, BenefitDetailRepository>();
builder.Services.AddScoped<IBenefitDetailService, BenefitDetailService>();

builder.Services.AddScoped<IContractPaymentHistoryRepository, ContractPaymentHistoryRepository>();
builder.Services.AddScoped<IContractPaymentHistoryService, ContractPaymentHistoryService>();

// Email - Verfication password
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService,EmailService>();


// Firebase service
builder.Services.AddScoped<FirebaseController>();

builder.Services.AddTransient(provider =>
{
    // Đọc thông tin cấu hình từ IConfiguration
    var firebaseConfig = new FirebaseConfig(builder.Configuration.GetSection("Firebase")["ApiKey"]);
    return firebaseConfig;
});

builder.Services.AddTransient(provider => new FirebaseStorage(builder.Configuration.GetSection("Firebase")["StorageBucket"]));

builder.Services.AddTransient<FirebaseAuthProvider>();

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Filter request required Authorized
builder.Services.AddScoped<JwtAuthorizeFilter>();

builder.Services.AddAuthorization();


// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecreteKey"]))
    });


// Cors
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("https://sandbox.vnpayment.vn",
                                "http://sandbox.vnpayment.vn",
                                "http://localhost:5173", 
                                "http://127.0.0.1:5173")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
        });
});

// VnPay
builder.Services.AddSingleton<IVnPayService, VnPayService>();

builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();
