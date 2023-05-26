using Contracts.InterService;
using Domain.Accessors.Entities;
using Domain.Accessors.EntityOperations;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Accessors;
using Persistence.Accessors.Entities;
using Persistence.Accessors.EntityOperations;
using Persistence.Services;
using Services;
using Services.Abstraction;
using Services.Abstraction.Auctions;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;
using Services.Abstraction.EntityCRUDServices.EntityDeleters;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;
using Services.Abstraction.LogReaders;
using Services.Abstraction.Search;
using Services.Auctions;
using Services.EntityCRUDServices.EntityCreators;
using Services.EntityCRUDServices.EntityCreators.EntityCreationValidators;
using Services.EntityCRUDServices.EntityDeleters;
using Services.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;
using Services.EntityCRUDServices.EntityReaders;
using Services.EntityCRUDServices.EntityUpdaters;
using Services.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;
using Services.EntityCRUDServices.EntityUpdaters.Validators;
using Services.LogReaders;
using Services.Search;
using Services.Static;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379", true);

    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Unitok"), b => b.MigrationsAssembly("UI")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/signIn");
        options.AccessDeniedPath = new PathString("/signIn");
    });

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddHostedService<AuctionBackgroundService>();
builder.Services.AddSignalR();

builder.Services.AddScoped<IStaticService, StaticService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IDtoCreator, DtoCreator>();
builder.Services.AddScoped<IFileIdGenerator, FileIdGenerator>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUrlHelper>(x =>
{
	var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
	var factory = x.GetRequiredService<IUrlHelperFactory>();
	return factory.GetUrlHelper(actionContext);
});

AddAccessors();
AddCrudServices();

AddCrudServices();
AddLogReaders();

builder.Services.AddScoped<ISearchEngine, ShittyEngine>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapAreaControllerRoute(name: "AdminPanel", areaName: "AdminPanel", pattern: "{area:exists}/{controller}/{action?}");

app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.MapHub<AuctionHub>("/auctionHub");

app.Run();

void AddAccessors()
{
    builder.Services.AddScoped<IDbAccessor, DbAccessor>();
    builder.Services.AddScoped<IDbUserAccessor, DbUserAccessor>();
    builder.Services.AddScoped<IDbAuctionAccessor, DbAuctionAccessor>();
    builder.Services.AddScoped<IDbBidAccessor, DbBidAccessor>();
    builder.Services.AddScoped<IDbTokenAccessor, DbTokenAccessor>();
    builder.Services.AddScoped<IDbAuctionFinishLogAccessor, DbAuctionFinishLogAccessor>();
    builder.Services.AddScoped<IDbAuctionParticipationLogAccessor, DbAuctionParticipationLogAccessor>();
    builder.Services.AddScoped<IDbTokenCreationLogAccessor, DbTokenCreationLogAccessor>();
}

void AddCrudServices()
{
    AddCreationServices();
    AddReadServices();
    AddUpdateServices();
    AddDeletionServices();
}

void AddCreationServices()
{
    AddCreationValidationServices();
    builder.Services.AddScoped<IBidCreator, BidCreator>();
    builder.Services.AddScoped<IAuctionCreator, AuctionCreator>();
    builder.Services.AddScoped<ITokenCreator, TokenCreator>();
}

void AddCreationValidationServices()
{
    builder.Services.AddScoped<ITokenCreationValidator, TokenCreationValidator>();
    builder.Services.AddScoped<IBidCreationValidator, BidCreationValidator>();
    builder.Services.AddScoped<IAuctionCreationValidator, AuctionCreationValidator>();
}

void AddReadServices()
{
    builder.Services.AddScoped<IAuctionReader, AuctionReader>();
    builder.Services.AddScoped<IBidReader, BidReader>();
    builder.Services.AddScoped<ITokenReader, TokenReader>();
    builder.Services.AddScoped<IUserReader, UserReader>();
}


void AddUpdateValidationServices()
{
    builder.Services.AddScoped<IAuctionUpdatingValidator, AuctionUpdatingValidator>();
    builder.Services.AddScoped<ITokenUpdatingValidator, TokenUpdatingValidator>();
    builder.Services.AddScoped<IUserUpdatingValidator, UserUpdatingValidator>();
}

void AddUpdateServices()
{
    AddUpdateValidationServices();
    builder.Services.AddScoped<IAuctionUpdater, AuctionUpdater>();
    builder.Services.AddScoped<ITokenUpdater, TokenUpdater>();
    builder.Services.AddScoped<IUserUpdater, UserUpdater>();
    builder.Services.AddScoped<IAuctionUpdater, AuctionUpdater>();
}

void AddDeletionServices()
{
    AddDeletionValidationServices();
    builder.Services.AddScoped<IBidDeleter, BidDeleter>();
    builder.Services.AddScoped<IAuctionDeleter, AuctionDeleter>();
    builder.Services.AddScoped<ITokenDeleter, TokenDeleter>();
}

void AddDeletionValidationServices()
{
    builder.Services.AddScoped<ITokenDeletionValidator, TokenDeletionValidator>();
    builder.Services.AddScoped<IBidDeletionValidator, BidDeletionValidator>();
    builder.Services.AddScoped<IAuctionDeletionValidator, AuctionDeletionValidator>();
    builder.Services.AddScoped<IUserUpdateValidator, UserUpdateValidator>();
    builder.Services.AddScoped<IAuctionUpdatingValidator, AuctionUpdatingValidator>();
}

void AddLogReaders()
{
    builder.Services.AddScoped<ILogReader, LogReader>();
    builder.Services.AddScoped<IAuctionFinishLogReader, AuctionFinishLogReader>();
    builder.Services.AddScoped<IAuctionParticipationLogReader, AuctionParticipationLogReader>();
    builder.Services.AddScoped<ITokenCreationLogReader, TokenCreationLogReader>();
}