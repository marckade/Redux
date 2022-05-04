var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Somewhat of a security concern. But since we are not doing POSTS im not concerned about it
app.Use((context, next) =>
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        return next.Invoke();
    });

// app.UseHttpsRedirection();
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
    
app.UseAuthorization();

app.MapControllers();

app.Run();
