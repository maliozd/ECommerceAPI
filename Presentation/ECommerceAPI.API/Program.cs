using ECommerceAPI.API.ColumnModel;
using ECommerceAPI.API.Extensions;
using ECommerceAPI.API.LoggerConfigurationHandler;
using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Persistence;
using ECommerceAPI.SignalR;
using ECommerceAPI.SignalR.Hubs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Olusturulacak token de�erini kimlerin/hangi originlerin/sitelerin kullanaca��n� belirledi�imiz de�erdir. -> www.random.com
            ValidateIssuer = true, //Olusturulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r. -> www.myapi.com -> bu proje
            ValidateLifetime = true, //Olusturulan token de�erinin s�resini kontrol edecek olan do�rulama
            ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�.--> simetrik key -- uygulamaya �zel unique key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),//byte
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false, //lifetameValidator delegatedir. de�i�kenler temsili
            NameClaimType = ClaimTypes.Name
        };
    });


builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllers();
//builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});
SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserName";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "UserName";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
//columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

var output = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {ActionName} {UserName} {NewLine}{Exception}";
Logger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", outputTemplate: output)
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("DBConnectionString"),
     sinkOptions: new MSSqlServerSinkOptions
     {
         AutoCreateSqlTable = true,
         TableName = "Logs",
     },
     appConfiguration: null,
     columnOptions: columnOpt

    )
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();
app.UseMiddleware<LogUserNameMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
//    LogContext.PushProperty("UserName", username);
//    await next();
//});

app.MapControllers();
app.MapHubs();
app.Run();
