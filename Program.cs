//using novaTentativa.Server;

//var builder = WebApplication.CreateBuilder(args);

////cors 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader()
//              .WithExposedHeaders("Access-Control-Allow-Origin");
//    });
//});

////controladores
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////###############################################################################################################################
////ao terminar a controller, sempre lembrar que ? peciso referenciar aqui
//builder.Services.AddHttpClient<Cantor_Server>(client =>
//{
//    client.BaseAddress = new Uri("https://guilhermeonrails.github.io/");
//});

//builder.Services.AddHttpClient<Cantor_Server>();


//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseDeveloperExceptionPage();
//}

//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseCors("AllowAllOrigins");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using novaTentativa.Server;
using novaTentativa.Server;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Adicionar serviços essenciais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar HttpClient para Cantor_Server
builder.Services.AddHttpClient<Cantor_Server>(client =>
{
    client.BaseAddress = new Uri("https://guilhermeonrails.github.io/");
});

// Construir a aplicação
var app = builder.Build();

// Configurações para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Middlewares essenciais
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Rodar a aplicação
app.Run();

