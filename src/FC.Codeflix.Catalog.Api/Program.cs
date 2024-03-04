using FC.Codeflix.Catalog.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppConnections().AddUseCases().AddAndConfigureControllers();

var app = builder.Build();
app.UseDocs();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { };
