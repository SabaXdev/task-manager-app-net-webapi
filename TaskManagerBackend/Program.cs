using TaskManagerBackend.Interfaces;
using TaskManagerBackend.Models;
using TaskManagerBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Setup Database Connection
builder.Services.AddSingleton<TaskManagerDbContext>(); // Setup context class
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Setup automapper
builder.Services.AddProblemDetails();  // Add this line
builder.Services.AddLogging();  //  Add this line
builder.Services.AddScoped<ITaskManagerServices, TaskManagerService>();   //Setup services

// Optional
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Connect to Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Your Angular app's URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

//Database and context
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


// Use CORS before routing
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
