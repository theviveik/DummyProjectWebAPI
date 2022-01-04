using Microsoft.OpenApi.Models;

namespace DummyProject
{
    public static class SwaggerExtensions
    {
        public static void SwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Dummy Project API",
                    Version = "v1",
                    Description = "An API to perform Dummy Project operations",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Vivek Kumar Sharma",
                        Email = "vivek12j@gmail.com",
                        Url = new Uri("https://twitter.com/theviveik")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Dummy Project API LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

        }

        public static void UseSwaggerUIExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dummy Project API V1");
            });
        }
    }
}

