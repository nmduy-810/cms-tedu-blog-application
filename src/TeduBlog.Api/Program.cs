using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using TeduBlog.Api;
using TeduBlog.Core.Domains.Identity;
using TeduBlog.Core.Models.Contents;
using TeduBlog.Core.SeedWorks;
using TeduBlog.Data;
using TeduBlog.Data.Repositories;
using TeduBlog.Data.SeedWorks;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

try
{
    // Config DBC and ASP.NET Core Identity
    builder.Services.AddDbContext<TeduBlogDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<TeduBlogDbContext>();
    
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });
    
    // Add Repository and UOF
    builder.Services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    // Business services and repositories
    /*builder.Services.AddScoped<IPostRepository, PostRepository>();*/
    
    // Fetch all types within the assembly where PostRepository is defined
    // Lấy tất cả các loại trong assembly nơi PostRepository được định nghĩa
    var services = typeof(PostRepository).Assembly.GetTypes()
        .Where(x => x.GetInterfaces().Any(i => i.Name == typeof(IRepositoryBase<,>).Name) &&
                                               x is { IsAbstract:false, IsClass:true, IsGenericType:false });
    foreach (var service in services)
    {
        // Retrieve all interfaces implemented by the current type
        // Lấy tất cả các interface mà loại hiện tại triển khai
        var allInterfaces = service.GetInterfaces();
        
        // Find the direct interface that the current type implements, excluding any derived interfaces
        // Tìm interface trực tiếp mà loại hiện tại triển khai, loại trừ bất kỳ interface phái sinh nào)
        var directInterface = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces())).FirstOrDefault();
        if (directInterface != null)
        {
            // Register the service with the DI container, using the direct interface as the service type
            // Đăng ký dịch vụ với DI container, sử dụng interface trực tiếp làm loại dịch vụ
            builder.Services.Add(new ServiceDescriptor(directInterface, service, ServiceLifetime.Scoped));
        }
    }

    // Add auto mapper
    builder.Services.AddAutoMapper(typeof(PostInListDto));
    
    //Default config for ASP.NET Core
    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
        c.SwaggerDoc("AdminAPI", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "API for Administrators",
            Description = "API for CMS core domain. This domain keeps track of campaigns, campaign rules, and campaign execution."
        });
        c.ParameterFilter<SwaggerNullableParameterFilter>();
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("AdminAPI/swagger.json", "Admin API");
            c.DisplayOperationId(); // Show function name in swagger
            c.DisplayRequestDuration();
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    
    // Seeding data
    app.MigrationDatabase();

    app.Run();
}
catch (Exception e)
{
    var type = e.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
}
