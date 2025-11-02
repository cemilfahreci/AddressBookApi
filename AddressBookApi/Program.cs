using AddressBookApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Address Book API",
        Version = "v1",
        Description = "Address Book REST API - Create, Read, Update, Delete and Search addresses",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Address Book API",
            Email = "support@addressbookapi.com"
        }
    });
    c.MapType(typeof(void), () => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string", Nullable = true });
});

builder.Services.AddSingleton<IAddressService, AddressService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address Book API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
