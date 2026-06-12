using Asp.Versioning;

namespace API.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection
            AddApiVersioningConfiguration(
            this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion =
                    new ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified =
                    true;

                options.ReportApiVersions = true;
            });

            return services;
        }
    }
}
