using Serilog;
using Simple_BlogUI;
using Simple_BlogUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var apiBaseUrl = builder.Configuration["ApiConnections:ApiBaseUrl"];
var displayPerPage = builder.Configuration["WebSettings:DisplayPerPage"];
if (int.TryParse(displayPerPage, out int value) == true)
    BlogConstant.DisplayPerPage = value;

builder.Services.AddHttpClient<IBlogService, BlogService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
