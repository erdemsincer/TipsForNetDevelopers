using DataAccess.Context;
using DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

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
app.Use(async (context,next)=>
{ 

	try
    {
        await next(context);
    }
    catch (Exception E)
    {
        AppDbContext _context = new();
        ErrorLog errorLog = new()
        {
            MethodName = context.Request.Path.Value,
            Trace = E.StackTrace,
            CreatedDate = DateTime.Now,
        };
        await _context.ErrorLogs.AddAsync(errorLog);
        await _context.SaveChangesAsync();
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(E.Message);
	
    }

});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
