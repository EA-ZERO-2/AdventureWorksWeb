using Microsoft.AspNetCore.Builder;

namespace AdventureWorksWeb
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            services.AddRazorPages();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseDefaultFiles(); //index.html, default.html, index.asp, index.cshtml
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGet("/hola", () => "Hola Mundo!");
            });

        }
    }
}
