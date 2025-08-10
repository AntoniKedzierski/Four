using Four.Presentation.Components;
using Four.Tests;

internal class Program {

    private static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();


        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        for (int i = 0; i < 10; ++i) {
            Console.WriteLine("========================");
            Console.WriteLine($"Game {i + 1}:");
            var game = DealGenerator.CreateGame();
            Console.Write(game.ToString());
            Console.WriteLine("========================");
        }

        app.Run();
    }
}