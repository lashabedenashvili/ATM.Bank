using ATM.Bank.Aplication.AddressServ;
using ATM.Bank.Aplication.ContactInformationServ;
using ATM.Bank.Aplication.PrivateInformationServ;
using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.BillServ;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContext,Context>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactInformationService, ContactInformationService>();
builder.Services.AddScoped<IPrivateInformationService, PrivateInformationService>();
builder.Services.AddScoped<IAddressService,AddressService>();
builder.Services.AddScoped<IBillService,BillService>();



builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
