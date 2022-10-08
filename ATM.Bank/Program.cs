using ATM.Bank.Aplication.AddressServ;
using ATM.Bank.Aplication.ContactInformationServ;
using ATM.Bank.Aplication.PrivateInformationServ;
using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.ATMServ;
using ATM.Bank.Aplication.Service.BillServ;
using ATM.Bank.Aplication.Service.CardServ;
using ATM.Bank.Aplication.Service.LoggTimeServ;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
     Description="Standard Authorization header using  Bearer scheme, e.g. \"bearar{token}\"",
     In=ParameterLocation.Header,
     Name="Autorization",
     Type=SecuritySchemeType.ApiKey
    });

    x.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<IContext,Context>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactInformationService, ContactInformationService>();
builder.Services.AddScoped<IPrivateInformationService, PrivateInformationService>();
builder.Services.AddScoped<IAddressService,AddressService>();
builder.Services.AddScoped<IBillService,BillService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ILoggTimeService, LoggTimeService>();
builder.Services.AddScoped<IATMService, ATMService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration
            .GetSection("AppSettings:Token").Value)),
            ValidateIssuer=false,
            ValidateAudience=false,
        };
    });





builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

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

app.MapControllers();

app.Run();
