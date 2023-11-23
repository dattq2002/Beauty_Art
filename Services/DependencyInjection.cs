using Microsoft.Extensions.DependencyInjection;
using Services.Service;
using Services.Service.Interface;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection CoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentTimeService, CurrentTimeService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICourseServices, CourseServices>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
    }
}
