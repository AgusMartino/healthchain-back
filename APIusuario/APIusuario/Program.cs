using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["dev-ffhtfaal8oyklq6l.us.auth0.com"];
    options.ClientId = builder.Configuration["StQoCwCjogk9IdFv2h4LjNjliogRyosT"];
});
builder.Services.AddControllersWithViews();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
