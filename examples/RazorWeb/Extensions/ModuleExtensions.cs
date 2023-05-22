using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.AttributeBuilder;
using Piranha.Manager.Editor;
using RazorWeb.Models;

namespace RazorWeb.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddPiranhaExtend(this IServiceCollection services)
        {
            App.Blocks.Register<RawHtmlBlock>();
            App.Modules.Manager().Scripts.Add("~/manager/js/rawhtml-block.js");
            App.Modules.Manager().Styles.Add("~/manager/js/rawhtml-block.css");

            return services;
        }

        public static IApplicationBuilder UsePiranhaExtend(this IApplicationBuilder builder, IApi api)
        {
            builder.UsePiranha(options =>
            {
                // Initialize Piranha
                App.Init(api);

                App.Blocks.Register<RazorWeb.Models.RawHtmlBlock>();
                App.Modules.Manager().Scripts.Add("~/assets/js/rawhtml-block.js");
                App.Modules.Manager().Styles.Add("~/assets/css/rawhtml-block.css");

                // Build content types
                new ContentTypeBuilder(api)
                    .AddAssembly(typeof(Program).Assembly)
                    .Build()
                    .DeleteOrphans();

                // Configure Tiny MCE
                EditorConfig.FromFile("editorconfig.json");

                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity();
            });
            // Add the embedded resources.  All of the javascript files in the assets folder will be served
            // from the below request path, in our case this is only js files.
            return builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(ModuleExtensions).Assembly, "Argus.Web.Cms.Modules.assets"),
                RequestPath = "/manager/js"
            });
        }
    }
}
