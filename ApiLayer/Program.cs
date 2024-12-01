
using BusinessLayer.Manager;
using BusinessLayer.Service;
using BusinessLayer.Validation;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concret;
using DataTransferObject.DtoEntity;
using DataTransferObject.DtoProfile;
using EntityLayer.Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SahredLayer;
using Serilog;
using System.Reflection;
using System.Text;

namespace ApiLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* builder.Services.AddControllers().AddFluentValidation
                 (x => x.RegisterValidatorsFromAssemblyContaining<Contact>());*/

            builder.Services.AddControllers();
              //  .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ContactValidation>());




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));

            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
            //builder.Services.AddFluentValidation(typeof(Program).Assembly);


            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File("C:\\Logss\\applog-.txt", rollingInterval: RollingInterval.Day).MinimumLevel.Information()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Host.UseSerilog();


            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddTransient<IAuthService, AuthManager>();
            builder.Services.AddScoped<IEmailService,EmailManager>();
            builder.Services.AddScoped<ICategory, EFCategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<ICommentService, CommentManager>();
            builder.Services.AddScoped<IComment, EFCommentRepository>();
            builder.Services.AddScoped<IFavoriteProductService, FavoriteProductManager>();
            builder.Services.AddScoped<IFavoriteProduct, EFFavoriteProductRepository>();
            builder.Services.AddScoped<IOrderService, OrderManager>();
            builder.Services.AddScoped<IOrder, EFOrderRepository>();
            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<IProduct, EFProductRepository>();
            builder.Services.AddScoped<IUserService, UserManager>();
            builder.Services.AddScoped<IUserApp, EFUserRepository>();

            builder.Services.AddScoped<IFeedbackScoreService, FeedbackScoreManager>();
            builder.Services.AddScoped<IFeedbackScore, EFFeedbackScoreReository>();

            builder.Services.AddScoped<ITrendService, TrendManager>();
            builder.Services.AddScoped<ITrend, EFTrendRepository>();
            builder.Services.AddScoped<ITrendPrtial, EFTrendRepository>();
            builder.Services.AddScoped<IAbout, EFAboutRepository>();
            builder.Services.AddScoped<IAboutService, AboutManager>();
            builder.Services.AddScoped<IContact, EFContactRepository>();
            builder.Services.AddScoped<IContactService, ContactManager>();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("SmtpSettings"));


            builder.Services.AddScoped<IValidator<ContactDto>, ContactValidation>();
            builder.Services.AddScoped<IValidator<AboutDto>, AboutValidation>();
            builder.Services.AddScoped<IValidator<CategoryDto>,CategoryValidator>();
            builder.Services.AddScoped<IValidator<CommentDto>, CommentValidator>();
            builder.Services.AddScoped<IValidator<FavoriteProductDto>, FavoriteProductValidator>();
            builder.Services.AddScoped<IValidator<FeedbackScoreDto>, FeedbackScoreValidator>();
            builder.Services.AddScoped<IValidator<OrderDto>, OrderValidator>();
            builder.Services.AddScoped<IValidator<ProductDto>, ProductValidator>();
            builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();





            // For Identity  
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationContext>()
                            .AddDefaultTokenProviders();
            // Adding Authentication  
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                        // Adding Jwt Bearer  
                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                                ClockSkew = TimeSpan.Zero,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                            };
                        });





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
        }
    }
}
