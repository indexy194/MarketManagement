using WebApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL;
using CoreBusiness;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

app.Run();



