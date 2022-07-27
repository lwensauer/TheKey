using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheKey.Backend.Blog;
using TheKey.Backend.Hubs;
using TheKey.Backend.Persistence;
using TheKey.Backend.WordCounter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IWordCounter>(new WordCounterFromHtmlInput());
builder.Services.AddHostedService<PollBlogEntriesService>();
builder.Services.AddSingleton<IBlogEntryRepository, BlogEntryRepository>();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("ClientPermission");
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<BlogHub>("/hubs/blog");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();