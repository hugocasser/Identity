using Notes.Identity;
using Notes.Identity.Data;

public class Program
{
    public static void Main(string[] args)
    {
       var host = CreateHostBuilder(args).Build();
       using (var scope = host.Services.CreateScope())
       {
           var serviceProvider = scope.ServiceProvider;
           try
           {
               var context = serviceProvider.GetRequiredService<AuthDbContext>();
               DbInitializer.Initialize(context);
           }
           catch (Exception ex)
           {
               var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
               logger.LogError(ex, "An error occured while app initialization");
           }
       }
       host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartUp>();
            });
}