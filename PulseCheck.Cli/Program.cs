using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PulseCheck.Cli;
using PulseCheck.Core.Abstractions;
using PulseCheck.Core.Platform;

var builder = new HostApplicationBuilder();
builder.Services.AddSingleton<ICommandRunner, BashCommandRunner>();
builder.Services.AddSingleton<ICommandParser, BashOutputParser>();

builder.Services.AddTransient<App>();

var host = builder.Build();
var app = host.Services.GetRequiredService<App>();

app.Run();