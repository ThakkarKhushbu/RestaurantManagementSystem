using RestaurantManagementSystem.API.Helper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

WebApplication app = builder.Build();

_ = app.UseSwagger();
_ = app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
