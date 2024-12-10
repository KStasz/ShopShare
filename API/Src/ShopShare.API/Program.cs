using ShopShare.API;
using ShopShare.API.Hubs;
using ShopShare.Application;
using ShopShare.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseResponseCompression();
    app.UseExceptionHandler("/api/error");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseAuthentication();
    app.MapControllers();
    app.MapHub<ListItemsHub>("api/ListItems");
        //.RequireAuthorization();
    app.Run();
}
