using Function;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

WebHost.CreateDefaultBuilder(args)
	.ConfigureServices(Startup.ConfigureServices)
	.UseUrls("http://localhost:5000")
	.Configure(app =>
	{
		app.UseRouting();

		app.UseEndpoints(e =>
		{
			var handler = e.ServiceProvider.GetRequiredService<FunctionHandler>();

			e.MapPost("/", async c =>
			{
				var result = await handler.Handle(c.Request);
				await c.Response.WriteAsJsonAsync(result);
			});
		});
	})
	.Build()
	.Run();
	