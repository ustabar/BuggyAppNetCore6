using BuggyBits.Models;
using BuggyBits.ViewModels;
using BuggyBits.Common;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPrometheusCounters();
builder.Services.AddPrometheusAspNetCoreMetrics();
builder.Services.AddPrometheusHttpClientMetrics();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton(_ => new DataLayer());
builder.Services.AddSingleton<MetricReporter>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var server = new MetricServer(port: 9998);
server.Start();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHttpMetrics();
app.UseMetricServer();
app.UseMiddleware<ResponseMetricMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
