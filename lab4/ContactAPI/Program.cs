﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Laba4.Data;
using Laba4.Controllers;
using Microsoft.EntityFrameworkCore;
//gg
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("Contact"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
