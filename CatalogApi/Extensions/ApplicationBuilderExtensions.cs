﻿using CatalogApi.Context;
using Microsoft.AspNetCore.Identity;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomMiddlewares(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(opt => opt.AllowAnyOrigin());
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || env.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    public static void UseSeeder(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsStaging())
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                Seeder.Initialize(context, userManager).Wait();
            }
        }
    }
}
