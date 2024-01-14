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
using System.Reflection;

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

builder.Services.AddScoped<FirebaseController>();

// Email - verfication password
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService,EmailService>();

builder.Services.AddSingleton(provider =>
{
    // Đọc thông tin cấu hình từ IConfiguration
    var firebaseConfig = new FirebaseConfig(builder.Configuration.GetSection("Firebase")["ApiKey"]);
    return firebaseConfig;
});
builder.Services.AddSingleton(provider => new FirebaseStorage(builder.Configuration.GetSection("Firebase")["StorageBucket"]));
builder.Services.AddSingleton<FirebaseAuthProvider>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Filter request required Authorized
builder.Services.AddScoped<JwtAuthorizeFilter>();

builder.Services.AddAuthorization();


// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

// Cors
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173",
                                "http://localhost:5174", "http://127.0.0.1:5174")
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
